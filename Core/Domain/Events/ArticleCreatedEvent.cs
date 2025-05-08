using NewsApp.Core.Domain.Entities.ArticleEntity;

namespace NewsApp.Core.Domain.Events;

/// <summary>
/// Event raised when a new article is created in the system.
/// Contains the newly created article entity for subscribers to process.
/// </summary>
public sealed class ArticleCreatedEvent
{
    /// <summary>
    /// Gets or initializes the newly created article.
    /// Required property that must be set when instantiating the event.
    /// </summary>
    public required Article Article { get; init; }
}
