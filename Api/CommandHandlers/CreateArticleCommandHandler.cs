using System.Threading;
using System.Threading.Tasks;
using NewsApp.Core.Domain.Commands;
using NewsApp.Core.Domain.Entities.ArticleEntity;
using NewsApp.Core.Domain.Events;
using NewsApp.Core.SharedKernel;
using NewsApp.Infrastructure.Persistence;

namespace NewsApp.Api.CommandHandlers;

/// <summary>
/// Handles the creation of new articles based on command data.
/// </summary>
/// <param name="newsDbContext">The database context used to persist article data.</param>
internal sealed class CreateArticleCommandHandler(NewsDbContext newsDbContext) : ICommandHandler<CreateArticleCommand>
{
    /// <summary>
    /// Processes a command to create a new article.
    /// </summary>
    /// <param name="command">The command containing article data to create.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation if needed.</param>
    /// <returns>A result object indicating success or failure of the operation.</returns>
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
