using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using NewsApp.Core.Domain.Events;

namespace NewsApp.Api.BackgroundServices;

internal sealed class DomainEventHandler : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        DomainEventDispatcher.NewArticleCreated += OnNewArticleCreated;

        return Task.CompletedTask;
    }

    private static void OnNewArticleCreated(object? sender, ArticleCreatedEvent @event) =>
        Console.WriteLine($"""New article with title "{@event.Article.Title.Value}" created!""");

    public override void Dispose()
    {
        base.Dispose();

        DomainEventDispatcher.NewArticleCreated -= OnNewArticleCreated;
    }
}
