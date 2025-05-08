using System.Collections.Generic;

namespace NewsApp.Core.Domain.Entities.ArticleEntity;

/// <summary>
/// Represents the title of an article as a value object.
/// Enforces business rules for article titles.
/// </summary>
public sealed class Title : ValueObject<Title>
{
    private const string ErrorKey = "title";
    private const int MaxLength = 50;

    private Title(string value) =>
        Value = value;

    /// <summary>
    /// Gets the string value of the title.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Creates a new Title instance after validating the input string.
    /// </summary>
    /// <param name="title">The title text to validate.</param>
    /// <returns>
    /// A Result containing either a valid Title object or validation errors if:
    /// - The title is null, empty, or whitespace
    /// - The title exceeds the maximum length of 50 characters
    /// </returns>
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

    /// <summary>
    /// Provides the components that determine equality for Title objects.
    /// </summary>
    /// <returns>An enumerable of objects that together determine equality.</returns>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
