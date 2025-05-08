using System;
using NewsApp.Core.Domain.Entities.ArticleEntity;

namespace NewsApp.Core.Domain.Events;

public static class DomainEventDispatcher
{
    public static event EventHandler<ArticleCreatedEvent>? NewArticleCreated;

    public static void DispatchNewArticleCreatedEvent(Article article) =>
        NewArticleCreated?.Invoke(null, new ArticleCreatedEvent
        {
            Article = article
        });
}
