using ProjetoPagamentos.Domain.Entities;

namespace ProjetoPagamentos.Application.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetByDocumentAsync(string document);
        Task<List<Account>> GetAllAccountsByUserDocument(string document);
    }
}
