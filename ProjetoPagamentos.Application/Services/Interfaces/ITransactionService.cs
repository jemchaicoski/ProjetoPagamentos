using ProjetoPagamentos.Domain.Models.Requests;
using ProjetoPagamentos.Domain.Models.Responses;

namespace ProjetoPagamentos.Application.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<CreateTransactionResponse> ProcessCreditTransactionAsync(CreateCreditTransactionRequest request);
        Task<CreateTransactionResponse> ProcessDebitTransactionAsync(CreateDebitTransactionRequest request);
        Task<CreateTransactionResponse> ProcessReserveTransactionAsync(CreateReserveTransactionRequest request);
        Task<CreateTransactionResponse> ProcessTransferTransactionAsync(CreateTransferTransactionRequest request);
    }
}
