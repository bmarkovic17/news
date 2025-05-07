using System.Collections.Generic;

namespace NewsApp.Core.Domain.ArticleEntity;

public sealed class Title : ValueObject<Title>
{
    private const string ErrorKey = "title";
    private const int MaxLength = 50;

    private Title(string value) =>
        Value = value;

    public string Value { get; }

    public static Result<Title> Create(string? title)
    {
        Dictionary<string, ICollection<string>> errors = [];

        if (string.IsNullOrWhiteSpace(title))
            errors.Add(ErrorKey, ["invalid"]);
        else if (title.Length > MaxLength)
            errors.Add(ErrorKey, ["tooLong"]);

        var createTitleResult = Result<Title>.Create(
            valueFactory: () => new Title(title!),
            errors);

        return createTitleResult;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
