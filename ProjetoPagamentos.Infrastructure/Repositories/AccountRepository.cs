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

        public async Task<Account> GetByIdAsync(Guid id)
        {
            return await _context.Accounts.FindAsync(id);

        }
        public async Task UpdateAsync(Account entity)
        {
            _context.Accounts.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Account entity)
        {
            _context.Accounts.Remove(entity);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }
    }
}
