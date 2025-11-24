namespace Auth.Domain.Intefaces.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity entity);
    Task<TEntity?> GetByIdentifierAsync(string identifier);
    Task<List<TEntity>> GetAllAsync();
    void Update(TEntity entity);
    void Remove(TEntity entity);
}