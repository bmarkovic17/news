using NewsApp.Core.Domain.Entities.ArticleEntity;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace NewsApp.Infrastructure.Extensions;

/// <summary>
/// Provides extension methods for configuring Npgsql database context options.
/// </summary>
internal static class NpgsqlDbContextOptionsBuilderExtensions
{
    /// <summary>
    /// Maps .NET enum types to their corresponding PostgreSQL enum types.
    /// </summary>
    /// <param name="npgsqlDbContextOptionsBuilder">The Npgsql DB context options builder to configure.</param>
    public static void MapEnums(this NpgsqlDbContextOptionsBuilder npgsqlDbContextOptionsBuilder) =>
        npgsqlDbContextOptionsBuilder
            .MapEnum<ArticleStatus>("article_status", "news");
}
