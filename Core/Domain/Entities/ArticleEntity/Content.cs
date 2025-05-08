using System.Collections.Generic;

namespace NewsApp.Core.Domain.Entities.ArticleEntity;

public sealed class Content : ValueObject<Content>
{
    private Content(string? value) =>
        Value = value;

    public string? Value { get; }

    public static Result<Content> Create(string? content)
    {
        var createContentResult = Result<Content>.Success(
            new Content(content));

        return createContentResult;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
