using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewsApp.Api.BackgroundServices;
using NewsApp.Infrastructure.Extensions;

namespace NewsApp.Api.Extensions;

/// <summary>
/// Provides extension methods for configuring the web application during startup.
/// </summary>
internal static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Registers and configures all required services for the NewsApp application.
    /// </summary>
    /// <param name="builder">The web application builder to configure.</param>
    /// <exception cref="Exception">Thrown when the database connection string is not configured.</exception>
    public static void AddNewsApp(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuth();

        builder.ConfigureOpenTelemetry();

        var connectionString = builder.Configuration.GetSection("ConnectionString").Value;

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new Exception("DB connection string must be set!");

        if (builder.Environment.IsDevelopment())
            connectionString = $"{connectionString};Include Error Detail=true";

        builder.Services.AddNewsAppInfrastructure(connectionString);
        builder.Services.AddQueryHandlers();
        builder.Services.AddCommandHandlers();

        builder.Services.AddHostedService<DomainEventHandler>();
    }

    /// <summary>
    /// Configures the OpenTelemetry services if the OTLP exporter endpoint is configured.
    /// </summary>
    /// <param name="builder">The web application builder to configure.</param>
    private static void ConfigureOpenTelemetry(this WebApplicationBuilder builder)
    {
        if (string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]))
            return;

        builder.Services.UseOpenTelemetry();
    }
}
