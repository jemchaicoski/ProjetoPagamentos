using Microsoft.AspNetCore.Mvc;
using ProjetoPagamentos.Application.Repositories;
using ProjetoPagamentos.Application.Services.Interfaces;
using ProjetoPagamentos.Domain.Models.Requests;

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

        [HttpPost("debit")]
        public async Task<IActionResult> DebitTransaction([FromBody] CreateDebitTransactionRequest request)
        {
            try
            {
                var response = await _transactionService.ProcessDebitTransactionAsync(request);

                return CreatedAtAction(nameof(DebitTransaction), new { id = response.TransactionId }, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("reserve")]
        public async Task<IActionResult> ReserveTransaction([FromBody] CreateReserveTransactionRequest request)
        {
            try
            {
                var response = _transactionService.ProcessReserveTransactionAsync(request).Result;

                return CreatedAtAction(nameof(ReserveTransaction), new { id = response.TransactionId }, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> TransferTransaction([FromBody] CreateTransferTransactionRequest request)
        {
            try
            {
                var response = await _transactionService.ProcessTransferTransactionAsync(request);

                return CreatedAtAction(nameof(TransferTransaction), new { id = response.TransactionId }, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
