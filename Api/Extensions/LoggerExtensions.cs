using System;
using Microsoft.Extensions.Logging;

namespace NewsApp.Api.Extensions;

internal static partial class LoggerExtensions
{
    [LoggerMessage(LogLevel.Error, "An exception occurred while fetching articles")]
    public static partial void GetAllArticlesQueryError(this ILogger logger, Exception exception);

    [LoggerMessage(LogLevel.Error, "An exception occurred while creating a new article")]
    public static partial void CreateArticleCommandError(this ILogger logger, Exception exception);
}
