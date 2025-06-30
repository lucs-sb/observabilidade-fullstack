using Auth.Domain.Intefaces.Repositories;
using Auth.Infrastructure.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly AppDbContext _dbContext;

    public Repository(AppDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<TEntity>> GetAllAsync() => await _dbContext.Set<TEntity>().ToListAsync();

    public async Task<TEntity?> GetByIdentifierAsync(string identifier) => await _dbContext.Set<TEntity>().FirstOrDefaultAsync(e => EF.Property<string>(e, "Identifier") == identifier);

    public async Task AddAsync(TEntity entity) => await _dbContext.Set<TEntity>().AddAsync(entity);

    public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);

    public void Remove(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);
}