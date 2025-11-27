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

        /// <summary>
        /// Realiza uma transação de crédito.
        /// </summary>
        /// <param name="request">
        /// Objeto contendo:
        /// - <b>AccountId</b>: ID da conta que receberá o crédito.<br/>
        /// - <b>Amount</b>: Valor a ser creditado.<br/>
        /// - <b>Currency</b>: Código da moeda (0 = BRL, 1 = USD).<br/>
        /// - <b>ReferenceId</b>: Identificador idempotente da transação.<br/>
        /// </param>
        /// <returns>
        /// Objeto contendo:
        /// - <b>transactionId</b>: ID da transação.<br/>
        /// - <b>success</b>: Indica se a operação foi bem sucedida.<br/>
        /// - <b>errorMessage</b>: Mensagem de erro, caso exista.<br/>
        /// </returns>
        /// <response code="201">Transação criada com sucesso.</response>
        /// <response code="400">Erro de validação.</response>
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

        /// <summary>
        /// Realiza uma transação de débito.
        /// </summary>
        /// <param name="request">
        /// Objeto contendo:
        /// - <b>AccountId</b>: ID da conta a ser debitada.<br/>
        /// - <b>Amount</b>: Valor do débito.<br/>
        /// - <b>Currency</b>: Código da moeda (0 = BRL, 1 = USD).<br/>
        /// - <b>ReferenceId</b>: Identificador idempotente da transação.<br/>
        /// </param>
        /// <returns>
        /// Objeto contendo:
        /// - <b>transactionId</b>: ID da transação.<br/>
        /// - <b>success</b>: Indica se a operação foi bem sucedida.<br/>
        /// - <b>errorMessage</b>: Mensagem de erro, caso exista.<br/>
        /// </returns>
        /// <response code="201">Transação criada com sucesso.</response>
        /// <response code="400">Erro de validação.</response>
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

        /// <summary>
        /// Reserva saldo para uma transação futura (pré-autorização).
        /// </summary>
        /// <param name="request">
        /// Objeto contendo:
        /// - <b>AccountId</b>: ID da conta a ser reservada.<br/>
        /// - <b>Amount</b>: Valor da reserva.<br/>
        /// - <b>Currency</b>: Código da moeda (0 = BRL, 1 = USD).<br/>
        /// - <b>ReferenceId</b>: Identificador idempotente.<br/>
        /// </param>
        /// <returns>
        /// Objeto contendo:
        /// - <b>transactionId</b>: ID da reserva.<br/>
        /// - <b>success</b>: Indica se a operação foi bem sucedida.<br/>
        /// - <b>errorMessage</b>: Mensagem de erro, caso exista.<br/>
        /// </returns>
        /// <response code="201">Reserva criada com sucesso.</response>
        /// <response code="400">Erro de validação.</response>
        [HttpPost("reserve")]
        public async Task<IActionResult> ReserveTransaction([FromBody] CreateReserveTransactionRequest request)
        {
            try
            {
                var response = await _transactionService.ProcessReserveTransactionAsync(request);
                return CreatedAtAction(nameof(ReserveTransaction), new { id = response.TransactionId }, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Captura o valor previamente reservado.
        /// </summary>
        /// <param name="request">
        /// Objeto contendo:
        /// - <b>AccountId</b>: ID da conta a ser reservada.<br/>
        /// - <b>Amount</b>: Valor a capturar.<br/>
        /// - <b>Currency</b>: Código da moeda (0 = BRL, 1 = USD).<br/>
        /// - <b>ReferenceId</b>: Identificador idempotente.<br/>
        /// </param>
        /// <returns>
        /// Objeto contendo:
        /// - <b>transactionId</b>: ID da captura.<br/>
        /// - <b>success</b>: Indica se a operação foi bem sucedida.<br/>
        /// - <b>errorMessage</b>: Mensagem de erro, caso exista.<br/>
        /// </returns>
        /// <response code="201">Captura realizada.</response>
        /// <response code="400">Erro de validação.</response>
        [HttpPost("capture")]
        public async Task<IActionResult> CaptureTransaction([FromBody] CreateCaptureTransactionRequest request)
        {
            try
            {
                var response = await _transactionService.ProcessCaptureTransactionAsync(request);
                return CreatedAtAction(nameof(CaptureTransaction), new { id = response.TransactionId }, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Realiza uma transferência entre contas.
        /// </summary>
        /// <param name="request">
        /// Objeto contendo:
        /// - <b>HolderAccountId</b>: Conta origem.<br/>
        /// - <b>TargetAccountId</b>: Conta destino.<br/>
        /// - <b>Amount</b>: Valor da transferência.<br/>
        /// - <b>Currency</b>: Código da moeda (0 = BRL, 1 = USD).<br/>
        /// - <b>ReferenceId</b>: Identificador idempotente.<br/>
        /// </param>
        /// <returns>
        /// Objeto contendo:
        /// - <b>transactionId</b>: ID da transferência.<br/>
        /// - <b>success</b>: Indica se a operação foi bem sucedida.<br/>
        /// - <b>errorMessage</b>: Mensagem de erro, caso exista.<br/>
        /// </returns>
        /// <response code="201">Transferência concluída.</response>
        /// <response code="400">Erro de validação.</response>
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
