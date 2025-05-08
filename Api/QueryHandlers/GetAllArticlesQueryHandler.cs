using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewsApp.Core.Domain.Entities.ArticleEntity;
using NewsApp.Core.Domain.Queries;
using NewsApp.Core.SharedKernel;
using NewsApp.Infrastructure.Persistence;

namespace NewsApp.Api.QueryHandlers;

/// <summary>
/// Handles the retrieval of paginated articles from the database.
/// </summary>
/// <param name="newsDbContext">The database context used to access article data.</param>
internal sealed class GetAllArticlesQueryHandler(NewsDbContext newsDbContext) : QueryHandlerBase<GetAllArticlesQuery>
{
    /// <summary>
    /// Executes the query to retrieve a paginated list of articles.
    /// </summary>
    /// <param name="query">The query containing pagination parameters (page number and page size).</param>
    /// <returns>A <see cref="PagedResult{T}"/> containing the list of articles for the requested page.</returns>
    protected override async Task<ResultBase> RunAsync(GetAllArticlesQuery query)
    {
        var articles = await newsDbContext.Articles
            .OrderByDescending(article => article.Created)
            .Skip((query.Page - 1) * query.Size)
            .Take(query.Size)
            .ToListAsync();

        return PagedResult<List<Article>>.Success(articles, query.Page, query.Size);
    }
}
