using ProjetoPagamentos.Api.Models.Requests;
using ProjetoPagamentos.Api.Models.Responses;

namespace ProjetoPagamentos.Application.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<CreateCreditTransactionResponse> ProcessCreditTransactionAsync(CreateCreditTransactionRequest request);
    }
}
