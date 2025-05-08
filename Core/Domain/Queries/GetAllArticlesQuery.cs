using System.Collections.Generic;

namespace NewsApp.Core.Domain.Queries;

/// <summary>
/// Represents a query to retrieve a paginated list of all articles.
/// </summary>
/// <param name="page">The page number to retrieve (1-based).</param>
/// <param name="size">The number of articles per page.</param>
public sealed class GetAllArticlesQuery(int page, int size) : IQuery
{
    private const string PageErrorKey = "page";
    private const string SizeErrorKey = "size";

    public int Page { get; } = page;

    public int Size { get; } = size;

    /// <summary>
    /// Validates the query parameters.
    /// </summary>
    /// <returns>A result indicating whether the validation succeeded or failed with error messages.</returns>
    public ResultBase Validate()
    {
        Dictionary<string, ICollection<string>> errors = [];

        if (Page < 1)
            errors.Add(PageErrorKey, ["invalid"]);

        if (Size is < 1 or > 100)
            errors.Add(SizeErrorKey, ["invalid"]);

        var validateResult = Result.Create(errors);

        return validateResult;
    }
}
