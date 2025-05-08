using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NewsApp.Api.CommandHandlers;
using NewsApp.Api.Extensions;
using NewsApp.Api.QueryHandlers;
using NewsApp.Core.Domain.Commands;
using NewsApp.Core.Domain.Entities.ArticleEntity;
using NewsApp.Core.Domain.Queries;
using NewsApp.Core.SharedKernel;

namespace NewsApp.Api.Endpoints;

internal static class ArticleEndpoints
{
    public static RouteGroupBuilder MapPublicArticleEndpoints(this RouteGroupBuilder group)
    {
        _ = group
            .MapGroup("/article")
            .MapGetAllArticlesEndpoint();

        return group;
    }

    public static RouteGroupBuilder MapSecuredArticleEndpoints(this RouteGroupBuilder group)
    {
        _ = group
            .MapGroup("/article")
            .MapCreateNewArticleEndpoint();

        return group;
    }

    private static RouteGroupBuilder MapGetAllArticlesEndpoint(this RouteGroupBuilder group)
    {
        group
            .MapGet(
                "/",
                async Task<Results<Ok<PagedResult<List<Article>>>, BadRequest<Result>, InternalServerError>> (
                    ILogger<GetAllArticlesQuery> logger,
                    GetAllArticlesQueryHandler handler,
                    int page = 1,
                    int size = 10) =>
                {
                    try
                    {
                        var query = new GetAllArticlesQuery(page, size);
                        var getArticlesResult = await handler.HandleAsync(query);

                        return getArticlesResult.IsSuccessful
                            ? TypedResults.Ok((PagedResult<List<Article>>)getArticlesResult)
                            : TypedResults.BadRequest((Result)getArticlesResult);
                    }
                    catch (Exception exception)
                    {
                        logger.GetAllArticlesQueryError(exception);

                        return TypedResults.InternalServerError();
                    }
                })
            .CacheOutput(policy => policy
                .Expire(TimeSpan.FromHours(1)));

        return group;
    }

    private static RouteGroupBuilder MapCreateNewArticleEndpoint(this RouteGroupBuilder group)
    {
        group
            .MapPost(
                "/",
                async Task<Results<Ok<Result<Article>>, BadRequest<Result>, InternalServerError>>(
                    ILogger<CreateArticleCommand> logger,
                    ClaimsPrincipal user,
                    CreateArticleCommand command,
                    CreateArticleCommandHandler handler,
                    CancellationToken cancellationToken) =>
                {
                    try
                    {
                        if (int.TryParse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
                            command.UserId = userId;
                        else
                            return TypedResults.BadRequest(Result.Fail());

                        var createArticleResult = await handler.HandleAsync(command, cancellationToken);

                        return createArticleResult.IsSuccessful
                            ? TypedResults.Ok((Result<Article>)createArticleResult)
                            : TypedResults.BadRequest((Result)createArticleResult);
                    }
                    catch (Exception exception)
                    {
                        logger.CreateArticleCommandError(exception);

                        return TypedResults.InternalServerError();
                    }
                })
            .CacheOutput(policy => policy
                .Expire(TimeSpan.FromHours(1)));

        return group;
    }
}
