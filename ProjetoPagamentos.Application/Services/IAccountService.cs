using ProjetoPagamentos.Api.Models.Requests;
using ProjetoPagamentos.Api.Models.Responses;

namespace ProjetoPagamentos.Application.Services
{
    public interface IAccountService
    {
        Task<CreateAccountResponse> ProcessCreateAccountAsync(CreateAccountRequest request);
    }
}
