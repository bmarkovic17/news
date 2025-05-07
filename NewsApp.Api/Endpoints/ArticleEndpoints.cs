using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
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
                async ([FromBody] GetAllArticlesQuery query, GetAllArticlesQueryHandler handler) =>
                    await handler.HandleAsync(query));

        return group;
    }
}
