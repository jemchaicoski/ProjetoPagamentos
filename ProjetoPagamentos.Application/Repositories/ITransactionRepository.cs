using ProjetoPagamentos.Domain.Entities.Transactions;

namespace ProjetoPagamentos.Application.Repositories
{
    public interface ITransactionRepository
    {
        Task<string> CreateAsync(BaseTransaction transaction);
        Task<BaseTransaction> GetByIdAsync(string id);
        Task<List<BaseTransaction>> GetByAccountIdAsync(Guid accountId);
        Task<bool> DeleteAsync(string id);
    }
}
