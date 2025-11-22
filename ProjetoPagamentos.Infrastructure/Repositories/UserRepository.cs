using Microsoft.EntityFrameworkCore;
using ProjetoPagamentos.Domain.Entities;
using ProjetoPagamentos.Infrastructure.Persistence;
using System.Reflection.Metadata;

namespace ProjetoPagamentos.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByAccountGuidAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByDocumentAsync(string document)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByGuidAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public void Remove(User entity)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserExistsAsync(string document)
        {
            return _context.Users.AnyAsync(u => u.UserDocument.Document == document);
        }
    }
}
