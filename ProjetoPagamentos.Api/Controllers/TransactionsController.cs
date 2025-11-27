using Microsoft.AspNetCore.Mvc;
using ProjetoPagamentos.Api.Models.Requests;
using ProjetoPagamentos.Application.Repositories;
using ProjetoPagamentos.Application.Services.Interfaces;

namespace ProjetoPagamentos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService, IAccountRepository accountRepository)
        {
            _transactionService = transactionService;
        }

        [HttpPost("credit")]
        public async Task<IActionResult> CreditTransaction([FromBody] CreateCreditTransactionRequest request)
        {
            try
            {
                var response = await _transactionService.ProcessCreditTransactionAsync(request);

                return CreatedAtAction(nameof(CreditTransaction), new { id = response.TransactionId }, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
