using System.Collections.Generic;
using NewsApp.Core.Domain.ArticleEntity;

namespace NewsApp.Core.Domain.UserEntity;

public sealed class User
{
    private readonly List<Article> _articles = [];

    public required PersonalName PersonalName { get; init; }

    public required Email Email { get; init; }

    public IEnumerable<Article> Articles => _articles;
}
