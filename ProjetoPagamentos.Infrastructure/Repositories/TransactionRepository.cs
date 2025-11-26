using MongoDB.Driver;
using ProjetoPagamentos.Application.Repositories;
using ProjetoPagamentos.Domain.Entities.Transactions;
using ProjetoPagamentos.Domain.Enums;

namespace ProjetoPagamentos.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IMongoCollection<BaseTransaction> _transactions;

        public TransactionRepository(IMongoDatabase database)
        {
            _transactions = database.GetCollection<BaseTransaction>("Transactions");
        }

        public async Task<string> CreateAsync(BaseTransaction transaction)
        {
            transaction.ValidateTransaction();
            await _transactions.InsertOneAsync(transaction);
            return transaction.TransactionId;
        }

        public async Task<BaseTransaction> GetByIdAsync(string id)
        {
            return await _transactions
                .Find(t => t.TransactionId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<BaseTransaction>> GetByAccountIdAsync(Guid accountId)
        {
            return await _transactions
                .Find(t => t.AccountId == accountId)
                .ToListAsync();
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _transactions.DeleteOneAsync(
                t => t.TransactionId == id
            );

            return result.DeletedCount > 0;
        }
    }
}
