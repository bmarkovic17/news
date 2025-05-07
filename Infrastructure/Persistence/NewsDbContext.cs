using Microsoft.EntityFrameworkCore;

namespace NewsApp.Infrastructure.Persistence;

public sealed class NewsDbContext(DbContextOptions<NewsDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NewsDbContext).Assembly);
    }
}
