using System.Collections.Generic;
using NewsApp.Core.Domain.Entities.ArticleEntity;

namespace NewsApp.Core.Domain.Entities.UserEntity;

/// <summary>
/// Represents a user entity in the news application.
/// </summary>
public sealed class User
{
    /// <summary>
    /// The private collection of articles associated with this user.
    /// </summary>
    private readonly List<Article> _articles = [];

    public required PersonalName PersonalName { get; init; }

    public required Email Email { get; init; }

    public IEnumerable<Article> Articles => _articles;
}
