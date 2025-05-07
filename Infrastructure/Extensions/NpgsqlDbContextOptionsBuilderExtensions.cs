using NewsApp.Core.Domain.ArticleEntity;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace NewsApp.Infrastructure.Extensions;

internal static class NpgsqlDbContextOptionsBuilderExtensions
{
    public static void MapEnums(this NpgsqlDbContextOptionsBuilder npgsqlDbContextOptionsBuilder) =>
        npgsqlDbContextOptionsBuilder
            .MapEnum<ArticleStatus>("article_status", "news");
}
