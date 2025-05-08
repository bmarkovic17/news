using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NewsApp.Infrastructure.Extensions;

namespace NewsApp.Infrastructure.Persistence;

/// <summary>
/// Factory for creating instances of NewsDbContext at design time.
/// Used by EF Core tools for migrations and other design-time operations.
/// </summary>
internal sealed class NewsDbContextDesignTimeFactory : IDesignTimeDbContextFactory<NewsDbContext>
{
    /// <summary>
    /// Creates a new instance of a NewsDbContext.
    /// Reads connection string from user secrets.
    /// </summary>
    /// <param name="args">Arguments provided by the design-time service.</param>
    /// <returns>A new instance of NewsDbContext.</returns>
    /// <exception cref="Exception">Thrown when connection string is not set in user secrets.</exception>
    public NewsDbContext CreateDbContext(string[] args)
    {
        var connectionString = new ConfigurationBuilder()
            .AddUserSecrets(Assembly.GetExecutingAssembly(), optional: false)
            .Build()
            .GetSection("ConnectionString")
            .Value;

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new Exception("DB connection string must be set in a user secret!");

        var builder = new DbContextOptionsBuilder<NewsDbContext>()
            .UseNpgsql(
                $"{connectionString};Include Error Detail=true",
                options =>
                    options
                        .MigrationsHistoryTable("migration_history", "log")
                        .MapEnums())
            .UseSnakeCaseNamingConvention();

        return new NewsDbContext(builder.Options);
    }
}
