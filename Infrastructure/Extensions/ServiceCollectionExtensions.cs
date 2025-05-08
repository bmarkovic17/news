using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewsApp.Infrastructure.Persistence;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace NewsApp.Infrastructure.Extensions;

/// <summary>
/// Provides extension methods for configuring infrastructure services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds and configures the NewsApp infrastructure services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to add the infrastructure services to.</param>
    /// <param name="connectionString">The database connection string.</param>
    public static void AddNewsAppInfrastructure(this IServiceCollection services, string? connectionString) =>
        services.AddDbContextPool<NewsDbContext>(
            options =>
                options
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution)
                    .UseNpgsql(
                        connectionString,
                        npgsqlOptions => npgsqlOptions
                            .EnableRetryOnFailure()
                            .MapEnums())
                    .UseSnakeCaseNamingConvention());

    /// <summary>
    /// Configures OpenTelemetry services for the application.
    /// </summary>
    /// <param name="services">The service collection to add the OpenTelemetry services to.</param>
    public static void UseOpenTelemetry(this IServiceCollection services)
    {
        services.AddTelemetryHealthCheckPublisher();

        services
            .AddOpenTelemetry()
            .WithLogging()
            .WithMetrics(metrics => metrics
                .AddRuntimeInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddMeter("Microsoft.EntityFrameworkCore")
                .AddMeter("Microsoft.Extensions.Diagnostics.HealthChecks")
            )
            .WithTracing(tracing => tracing
                .AddAspNetCoreInstrumentation()
                .AddEntityFrameworkCoreInstrumentation())
            .UseOtlpExporter();
    }
}
