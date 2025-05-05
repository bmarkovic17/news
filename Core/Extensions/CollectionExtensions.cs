using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace NewsApp.Core.Extensions;

public static class CollectionExtensions
{
    public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this IEnumerable<T>? collection) =>
        collection is null || collection.Any() is false;
}
