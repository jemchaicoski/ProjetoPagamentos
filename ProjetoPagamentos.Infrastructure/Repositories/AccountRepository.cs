using Microsoft.EntityFrameworkCore;
using ProjetoPagamentos.Application.Repositories;
using ProjetoPagamentos.Domain.Entities;
using ProjetoPagamentos.Infrastructure.Persistence;

namespace ProjetoPagamentos.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;
        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Account>> GetAllAccountsByUserDocument(string document)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Account>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetByDocumentAsync(string document)
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Account entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Account entity)
        {
            throw new NotImplementedException();
        }
    }
}
