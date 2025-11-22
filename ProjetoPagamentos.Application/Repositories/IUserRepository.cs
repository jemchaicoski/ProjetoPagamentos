using ProjetoPagamentos.Domain.Entities;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByGuidAsync(Guid id);
    Task<User> GetByDocumentAsync(string document);
    Task<bool> UserExistsAsync(string document);
}