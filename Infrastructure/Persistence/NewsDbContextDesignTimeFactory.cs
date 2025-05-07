using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NewsApp.Infrastructure.Extensions;

namespace NewsApp.Infrastructure.Persistence;

internal sealed class NewsDbContextDesignTimeFactory : IDesignTimeDbContextFactory<NewsDbContext>
{
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
