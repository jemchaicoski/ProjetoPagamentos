using ProjetoPagamentos.Api.Models.Requests;
using ProjetoPagamentos.Api.Models.Responses;

namespace ProjetoPagamentos.Application.Services.Interfaces
{
    public interface IAccountService
    {
        Task<CreateAccountResponse> ProcessCreateAccountAsync(CreateAccountRequest request);
    }
}
