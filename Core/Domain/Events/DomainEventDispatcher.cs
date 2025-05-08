using System;
using NewsApp.Core.Domain.Entities.ArticleEntity;

namespace NewsApp.Core.Domain.Events;

/// <summary>
/// Central dispatcher for domain events in the application.
/// Facilitates loose coupling between event publishers and subscribers.
/// </summary>
public static class DomainEventDispatcher
{
    /// <summary>
    /// Event fired when a new article is created in the system.
    /// </summary>
    public static event EventHandler<ArticleCreatedEvent>? NewArticleCreated;

    /// <summary>
    /// Dispatches an event indicating that a new article has been created.
    /// </summary>
    /// <param name="article">The newly created article entity.</param>
    public static void DispatchNewArticleCreatedEvent(Article article) =>
        NewArticleCreated?.Invoke(null, new ArticleCreatedEvent
        {
            Article = article
        });
}
