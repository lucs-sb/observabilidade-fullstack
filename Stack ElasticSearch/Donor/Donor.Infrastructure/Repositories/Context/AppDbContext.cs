using Microsoft.EntityFrameworkCore;

namespace Donor.Infrastructure.Repositories.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Domain.Entities.Donor> Donor { get; set; }
}