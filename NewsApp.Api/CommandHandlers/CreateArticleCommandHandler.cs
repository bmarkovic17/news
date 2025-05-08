using System.Threading;
using System.Threading.Tasks;
using NewsApp.Core.Commands;
using NewsApp.Core.Domain.ArticleEntity;
using NewsApp.Core.SharedKernel;
using NewsApp.Infrastructure.Persistence;

namespace NewsApp.Api.CommandHandlers;

internal sealed class CreateArticleCommandHandler(NewsDbContext newsDbContext) : ICommandHandler<CreateArticleCommand>
{
    public async Task<ResultBase> HandleAsync(CreateArticleCommand command, CancellationToken cancellationToken)
    {
        var createArticleResult = Article.Create(command.Title, command.Content);

        if (createArticleResult.IsSuccessful is false)
            return Result.Fail(createArticleResult.Errors);

        await newsDbContext.Articles
            .AddAsync(createArticleResult.Value!, cancellationToken)
            .ConfigureAwait(false);

        await newsDbContext
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return createArticleResult;
    }
}
