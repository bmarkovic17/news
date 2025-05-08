using System.Collections.Generic;

namespace NewsApp.Core.Domain.Entities.ArticleEntity;

/// <summary>
/// Represents the content of an article as a value object.
/// </summary>
public sealed class Content : ValueObject<Content>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Content"/> class.
    /// </summary>
    /// <param name="value">The string content value.</param>
    private Content(string? value) =>
        Value = value;

    /// <summary>
    /// Gets the string value of the content.
    /// </summary>
    public string? Value { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="Content"/> class.
    /// </summary>
    /// <param name="content">The string content to encapsulate.</param>
    /// <returns>A result containing the created content instance if successful.</returns>
    public static Result<Content> Create(string? content)
    {
        var createContentResult = Result<Content>.Success(
            new Content(content));

        return createContentResult;
    }

    /// <summary>
    /// Gets the components used for equality comparison.
    /// </summary>
    /// <returns>An enumerable of objects used for equality comparison.</returns>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
