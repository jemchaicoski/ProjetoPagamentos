using ProjetoPagamentos.Domain.Models.Requests;
using ProjetoPagamentos.Domain.Models.Responses;

namespace ProjetoPagamentos.Application.Services.Interfaces
{
    public interface IAccountService
    {
        Task<CreateAccountResponse> ProcessCreateAccountAsync(CreateAccountRequest request);
    }
}
