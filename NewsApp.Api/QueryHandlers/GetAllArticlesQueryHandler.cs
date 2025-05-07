using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewsApp.Api.Extensions;
using NewsApp.Core.Domain.ArticleEntity;
using NewsApp.Core.Queries;
using NewsApp.Core.SharedKernel;
using NewsApp.Infrastructure.Persistence;

namespace NewsApp.Api.QueryHandlers;

internal sealed class GetAllArticlesQueryHandler(NewsDbContext newsDbContext, ILogger<GetAllArticlesQueryHandler> logger) : IQueryHandler<GetAllArticlesQuery, List<Article>>
{
    public async Task<Result<List<Article>>> HandleAsync(GetAllArticlesQuery query)
    {
        try
        {
            var articles = await newsDbContext.Articles.ToListAsync();

            return Result<List<Article>>.Success(articles);
        }
        catch (Exception exception)
        {
            logger.GetAllArticlesQueryHandlerError(exception);

            return Result<List<Article>>.Fail();
        }
    }
}
