using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using NewsApp.Api.QueryHandlers;
using NewsApp.Core.Queries;

namespace NewsApp.Api.Endpoints;

internal static class ArticleEndpoints
{
    public static RouteGroupBuilder MapArticleEndpoints(this RouteGroupBuilder group)
    {
        _ = group
            .MapGroup("/article")
            .MapGetAllArticlesEndpoint();

        return group;
    }

    private static RouteGroupBuilder MapGetAllArticlesEndpoint(this RouteGroupBuilder group)
    {
        group
            .MapGet(
                "/",
                async (GetAllArticlesQueryHandler handler, int page = 1, int size = 10) =>
                {
                    var query = new GetAllArticlesQuery(page, size);
                    var articles = await handler.HandleAsync(query);

                    return articles;
                });

        return group;
    }
}
