using Microsoft.Extensions.DependencyInjection;
using NewsApp.Api.CommandHandlers;
using NewsApp.Api.QueryHandlers;

namespace NewsApp.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    public static void AddQueryHandlers(this IServiceCollection services) =>
        _ = services.AddScoped<GetAllArticlesQueryHandler>();

    public static void AddCommandHandlers(this IServiceCollection services) =>
        _ = services.AddScoped<CreateArticleCommandHandler>();
}
