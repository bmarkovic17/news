using System.Collections.Generic;

namespace NewsApp.Core.Queries;

public sealed class GetAllArticlesQuery(int page, int size) : IQuery
{
    private const string PageErrorKey = "page";
    private const string SizeErrorKey = "size";

    public int Page { get; } = page;

    public int Size { get; } = size;

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
