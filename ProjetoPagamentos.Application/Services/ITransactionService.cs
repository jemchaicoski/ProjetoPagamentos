using ProjetoPagamentos.Domain.Entities.Transactions;

namespace ProjetoPagamentos.Application.Services
{
    public interface ITransactionService
    {
        Task<string> ProcessCreditTransaction(Guid accountId, decimal amount);
    }
}
