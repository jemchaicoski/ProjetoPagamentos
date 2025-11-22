using Microsoft.AspNetCore.Mvc;
using ProjetoPagamentos.Api.Models.Requests;
using ProjetoPagamentos.Api.Models.Responses;
using ProjetoPagamentos.Application.Repositories;
using ProjetoPagamentos.Domain.Entities;

namespace ProjetoPagamentos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;

        public AccountsController(
            IAccountRepository accountRepository,
            IUserRepository userRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
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

                var response = new CreateAccountResponse
                {
                    Id = account.Id,
                    UserId = account.UserId,
                    AvailableBalance = account.AvailableBalance,
                    ReservedBalance = account.ReservedBalance,
                    CreditLimit = account.CreditLimit,
                    AccountStatus = account.AccountStatus.ToString()
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
