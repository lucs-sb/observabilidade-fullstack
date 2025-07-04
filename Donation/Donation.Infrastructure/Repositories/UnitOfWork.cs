using Donation.Domain.Intefaces.Repositories;
using Donation.Infrastructure.Repositories.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace Donation.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(AppDbContext context) => _context = context;

    public async ValueTask BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            Dispose();
        }
    }

    public async Task RollbackAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            Dispose();
        }
    }

    public IRepository<TEntity> Repository<TEntity>() where TEntity : class =>
        new Repository<TEntity>(_context);

    public void Dispose() => _transaction?.Dispose();
}