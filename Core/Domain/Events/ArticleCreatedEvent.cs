using NewsApp.Core.Domain.Entities.ArticleEntity;

namespace NewsApp.Core.Domain.Events;

public sealed class ArticleCreatedEvent
{
    public required Article Article { get; init; }
}
