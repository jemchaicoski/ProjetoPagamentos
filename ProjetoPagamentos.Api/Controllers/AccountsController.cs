using Microsoft.AspNetCore.Mvc;
using ProjetoPagamentos.Api.Models.Requests;
using ProjetoPagamentos.Api.Models.Responses;
using ProjetoPagamentos.Application.Repositories;
using ProjetoPagamentos.Domain.Entities;
using ProjetoPagamentos.Domain.Entities.Transactions;
using ProjetoPagamentos.Infrastructure.Repositories;

namespace ProjetoPagamentos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITransactionRepository _transactionRepository;

        public AccountsController(
            IAccountRepository accountRepository,
            IUserRepository userRepository,
            ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request)
        {
            try {
                if(request.UserId == Guid.Empty) return BadRequest("ID inválido");

                if (request.creditValue <= 0) return BadRequest("Valor Inicial não pode ser igual ou menor que 0R$");

                var user = await _userRepository.GetByIdAsync(request.UserId);
                if (user == null) return BadRequest("Usuário não encontrado");

                var account = new Account(request.UserId);
                await _accountRepository.AddAsync(account);

                var creditTransaction = new CreditTransaction(
                    accountId: account.Id,
                    amount: request.creditValue
                );

                var transactionId = await _transactionRepository.CreateAsync(creditTransaction);

                if (transactionId == null)
                {
                    await _accountRepository.DeleteAsync(account);
                    return BadRequest("Não foi possível realizar operação de crédito para criação de conta");
                }

                account.AvailableBalance = request.creditValue;
                await _accountRepository.UpdateAsync(account);

                var response = new CreateAccountResponse
                {
                    Id = account.Id,
                    UserId = account.UserId,
                    AvailableBalance = account.AvailableBalance,
                    ReservedBalance = account.ReservedBalance,
                    CreditLimit = account.CreditLimit,
                    AccountStatus = account.AccountStatus.ToString(),
                    TransactionId = transactionId
                };

                return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(Guid id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null) return NotFound();

            var response = new CreateAccountResponse
            {
                Id = account.Id,
                UserId = account.UserId,
                AvailableBalance = account.AvailableBalance,
                ReservedBalance = account.ReservedBalance,
                CreditLimit = account.CreditLimit,
                AccountStatus = account.AccountStatus.ToString()
            };

            return Ok(response);
        }
    }

}
