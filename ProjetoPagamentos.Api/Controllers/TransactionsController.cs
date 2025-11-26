using Microsoft.AspNetCore.Mvc;
using ProjetoPagamentos.Application.Repositories;
using ProjetoPagamentos.Domain.Entities.Transactions;

namespace ProjetoPagamentos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionsController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpPost("credit")]
        public async Task<IActionResult> CreditTransaction([FromBody] CreateCreditTransactionRequest request)
        {
            try
            {
                var transaction = new CreditTransaction(
                    request.AccountId,
                    request.Amount);

                var transactionId = await _transactionRepository.CreateAsync(transaction);

                return Ok(new
                {
                    TransactionId = transactionId,
                    Message = "Transação de crédito criada com sucesso"
                });
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
            catch (Exception ex)
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

    // DTO para a request
    public class CreateCreditTransactionRequest
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
