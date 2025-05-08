using System.Threading;
using System.Threading.Tasks;
using NewsApp.Core.Domain.Commands;
using NewsApp.Core.Domain.Entities.ArticleEntity;
using NewsApp.Core.Domain.Events;
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

        if (command.UserId is null or < 1)
            return Result.Fail();

        var article = createArticleResult.Value!;

        await newsDbContext.Articles
            .AddAsync(article, cancellationToken)
            .ConfigureAwait(false);

        newsDbContext.Entry(article).Property("UserId").CurrentValue = command.UserId;

        await newsDbContext
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        DomainEventDispatcher.DispatchNewArticleCreatedEvent(article);

        return createArticleResult;
    }
}
