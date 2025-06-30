using Microsoft.EntityFrameworkCore;

namespace Donor.Infrastructure.Repositories;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Donor> Donor { get; set; }
}