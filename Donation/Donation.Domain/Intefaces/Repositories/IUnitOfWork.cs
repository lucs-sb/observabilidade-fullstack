namespace Donation.Domain.Intefaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    ValueTask BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
    IRepository<TEntity> Repository<TEntity>() where TEntity : class;
}