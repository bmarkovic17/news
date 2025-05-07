using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewsApp.Infrastructure.Persistence;

namespace NewsApp.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddNewsAppInfrastructure(this IServiceCollection services, string? connectionString) =>
        services.AddDbContextPool<NewsDbContext>(
            options =>
                options
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution)
                    .UseNpgsql(
                        connectionString,
                        npgsqlOptions => npgsqlOptions.EnableRetryOnFailure())
                    .UseSnakeCaseNamingConvention());
}
