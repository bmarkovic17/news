using Microsoft.Extensions.DependencyInjection;
using NewsApp.Api.CommandHandlers;
using NewsApp.Api.QueryHandlers;

namespace NewsApp.Api.Extensions;

/// <summary>
/// Provides extension methods for configuring API-specific services in the dependency injection container.
/// </summary>
internal static class ServiceCollectionExtensions
{
    /// <summary>
    /// Configures authentication and authorization services for the application.
    /// </summary>
    /// <param name="services">The service collection to add the auth services to.</param>
    public static void AddAuth(this IServiceCollection services)
    {
        services
            .AddAuthentication()
            .AddJwtBearer();

        services.AddAuthorization();
    }

    /// <summary>
    /// Registers all query handlers as scoped services in the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to add the query handlers to.</param>
    public static void AddQueryHandlers(this IServiceCollection services) =>
        _ = services.AddScoped<GetAllArticlesQueryHandler>();

    /// <summary>
    /// Registers all command handlers as scoped services in the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to add the command handlers to.</param>
    public static void AddCommandHandlers(this IServiceCollection services) =>
        _ = services.AddScoped<CreateArticleCommandHandler>();
}
