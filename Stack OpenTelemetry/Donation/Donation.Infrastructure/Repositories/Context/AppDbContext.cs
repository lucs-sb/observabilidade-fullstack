using Microsoft.EntityFrameworkCore;

namespace Donation.Infrastructure.Repositories.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Domain.Entities.Donation> Donation { get; set; }
}