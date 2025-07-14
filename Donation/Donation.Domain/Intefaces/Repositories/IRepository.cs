namespace Donation.Domain.Intefaces.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity entity);
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<TEntity?> GetByBagNumberAsync(string bagNumber);
    Task<List<TEntity>> GetAllByDonorIdAsync(Guid donorId);
    void Update(TEntity entity);
    void Remove(TEntity entity);
}