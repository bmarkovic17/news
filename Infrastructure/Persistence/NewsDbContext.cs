using Microsoft.EntityFrameworkCore;
using NewsApp.Core.Domain.ArticleEntity;

namespace NewsApp.Infrastructure.Persistence;

public sealed class NewsDbContext(DbContextOptions<NewsDbContext> options) : DbContext(options)
{
    public DbSet<Article> Articles { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NewsDbContext).Assembly);
    }
}
