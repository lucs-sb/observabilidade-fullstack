namespace Donor.Domain.Intefaces.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity entity);
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<TEntity?> GetByEmailAsync(string email);
    Task<List<TEntity>> GetAllAsync();
    void Update(TEntity entity);
    void Remove(TEntity entity);
}