using Microsoft.Extensions.DependencyInjection;
using NewsApp.Api.QueryHandlers;

namespace NewsApp.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    public static void AddQueryHandlers(this IServiceCollection services) =>
        _ = services.AddScoped<GetAllArticlesQueryHandler>();
}
