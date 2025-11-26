using Microsoft.AspNetCore.Mvc;
using ProjetoPagamentos.Api.Models.Requests;
using ProjetoPagamentos.Api.Models.Responses;
using ProjetoPagamentos.Application.Repositories;
using ProjetoPagamentos.Domain.Entities.Transactions;
using System.Security.Principal;

namespace ProjetoPagamentos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;

        public TransactionsController(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }

        [HttpPost("credit")]
        public async Task<IActionResult> CreditTransaction([FromBody] CreateCreditTransactionRequest request)
        {
            try
            {
                var transaction = new CreditTransaction(
                    request.AccountId,
                    request.Amount);

                var account = _accountRepository.GetByIdAsync(request.AccountId).Result;

                if (account == null)
                    BadRequest("Conta não encontrada para operação de crédito");

                account!.AvailableBalance += request.Amount;
                await _accountRepository.UpdateAsync(account);
                var transactionId = await _transactionRepository.CreateAsync(transaction);

                var response = new CreateCreditTransactionResponse { TransactionId = transactionId };
                return CreatedAtAction(nameof(GetTransactionById), new { id = transactionId }, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("account/{accountId}")]
        public async Task<IActionResult> GetTransactionsByAccount(Guid accountId)
        {
            try
            {
                var transactions = await _transactionRepository.GetByAccountIdAsync(accountId);
                return Ok(transactions);
            }
            catch
            {
                return BadRequest(new { Error = "Não foi possível buscar transações" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(string id)
        {
            try
            {
                var transaction = await _transactionRepository.GetByIdAsync(id);

                if (transaction == null)
                    return NotFound();

                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
