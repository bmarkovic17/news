using System;
using System.Collections.Generic;
using NewsApp.Core.SharedKernel;

namespace NewsApp.Core.Domain.ArticleEntity;

public sealed class Article
{
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

    public Result Update(Title title, Content content)
    {
        if (title == Title && content == Content)
            return Result.Success();

        Title = title;
        Content = content;
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
