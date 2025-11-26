using ProjetoPagamentos.Api.Models.Requests;
using ProjetoPagamentos.Api.Models.Responses;

namespace ProjetoPagamentos.Application.Services
{
    public interface ITransactionService
    {
        Task<CreateCreditTransactionResponse> ProcessCreditTransactionAsync(CreateCreditTransactionRequest request);
    }
}
