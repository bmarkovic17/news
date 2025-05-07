using System;
using System.Collections.Generic;
using NewsApp.Core.SharedKernel;

namespace NewsApp.Core.Domain.ArticleEntity;

public sealed class Article
{
    // Used by EF only
    // Can be removed once https://github.com/dotnet/efcore/issues/31621 is implemented
    private Article()
    {
        Title = default!;
        Content = default!;
    }

    private Article(Title title, Content content)
    {
        Id = default;
        Title = title;
        Content = content;
        Status = ArticleStatus.Draft;
        Created = DateTimeOffset.UtcNow;
    }

    public int Id { get; }

    public Title Title { get; private set; }

    public Content Content { get; private set; }

    public ArticleStatus Status { get; private set; }

    public DateTimeOffset Created { get; }

    public DateTimeOffset? Modified { get; private set; }

    public DateTimeOffset? Published { get; private set; }

    public static Result<Article> Create(string? title, string? content)
    {
        var titleCreateResult = Title.Create(title);
        var contentCreateResult = Content.Create(content);

        var articleCreateResult = Result<Article>.Create(
            () => new Article(titleCreateResult.Value!, contentCreateResult.Value!),
            titleCreateResult.Errors,
            contentCreateResult.Errors);

        return articleCreateResult;
    }

    public Result Update(string title, string? content)
    {
        var createTitleResult = Title.Create(title);
        var createContentResult = Content.Create(content);

        var validParameters =
            createTitleResult.IsSuccessful &&
            createContentResult.IsSuccessful;

        if (validParameters is false)
            return Result.Fail(createTitleResult.Errors, createContentResult.Errors);

        if (createTitleResult.Value == Title && createContentResult.Value == Content)
            return Result.Success();

        Title = createTitleResult.Value!;
        Content = createContentResult.Value!;
        Modified = DateTimeOffset.UtcNow;

        return Result.Success();
    }

    public Result Publish()
    {
        if (Status == ArticleStatus.Published)
            return Result.Success();

        Status = ArticleStatus.Published;
        Modified = DateTimeOffset.UtcNow;
        Published = DateTimeOffset.UtcNow;

        return Result.Success();
    }

    public Result Unpublish()
    {
        switch (Status)
        {
            case ArticleStatus.Draft:
                return Result.Fail(new Dictionary<string, ICollection<string>>
                {
                    [ "status" ] = [ "cannotUnpublishDraft"]
                });

            case ArticleStatus.Unpublished:
                return Result.Success();

            case ArticleStatus.Published:
                Status = ArticleStatus.Unpublished;
                Modified = DateTimeOffset.UtcNow;
                Published = null;

                return Result.Success();

            default:
                return Result.Fail(new Dictionary<string, ICollection<string>>
                {
                    [ "status" ] = [ "error"]
                });
        }
    }
}
