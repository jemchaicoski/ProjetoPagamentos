using ProjetoPagamentos.Application.Repositories;
using ProjetoPagamentos.Domain.Entities.Transactions;

namespace ProjetoPagamentos.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepo;

        public TransactionService(ITransactionRepository transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }

        public async Task<string> ProcessCreditTransaction(Guid accountId, decimal amount)
        {
            var transaction = new CreditTransaction(accountId, amount);
            return await _transactionRepo.CreateAsync(transaction);
        }
    }
}
