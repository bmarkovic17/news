using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewsApp.Core.Domain.ArticleEntity;
using NewsApp.Core.Queries;
using NewsApp.Core.SharedKernel;
using NewsApp.Infrastructure.Persistence;

namespace NewsApp.Api.QueryHandlers;

internal sealed class GetAllArticlesQueryHandler(NewsDbContext newsDbContext) : QueryHandlerBase<GetAllArticlesQuery>
{
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
