using Microsoft.EntityFrameworkCore;
using TDGaming.Domain.Entities;

namespace TDGaming.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<VideoGame> Games => Set<VideoGame>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VideoGame>().HasKey(g => g.Id);
    }
}
