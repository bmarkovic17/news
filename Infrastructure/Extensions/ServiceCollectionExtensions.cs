using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewsApp.Infrastructure.Persistence;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

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
                        npgsqlOptions => npgsqlOptions
                            .EnableRetryOnFailure()
                            .MapEnums())
                    .UseSnakeCaseNamingConvention());

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
