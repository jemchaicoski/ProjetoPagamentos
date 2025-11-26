using ProjetoPagamentos.Entities;

public interface IRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task<bool> DeleteAsync(T entity);
    Task<bool> ExistsAsync(Guid id);
}