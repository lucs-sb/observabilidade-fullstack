using Donation.Domain.Intefaces.Repositories;
using Donation.Infrastructure.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace Donation.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly AppDbContext _dbContext;

    public Repository(AppDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<TEntity>> GetAllByDonorIdAsync(Guid donorId) => await _dbContext.Set<TEntity>().Where(e => EF.Property<Guid>(e, "DonorId") == donorId).ToListAsync();

    public async Task<TEntity?> GetByIdAsync(Guid id) => await _dbContext.Set<TEntity>().FindAsync(id);

    public async Task<TEntity?> GetByBagNumberAsync(string bagNumber) => await _dbContext.Set<TEntity>().FirstOrDefaultAsync(e => EF.Property<string>(e, "BagNumber") == bagNumber);

    public async Task AddAsync(TEntity entity) => await _dbContext.Set<TEntity>().AddAsync(entity);

    public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);

    public void Remove(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);
}