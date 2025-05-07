using System;
using Microsoft.Extensions.Logging;

namespace NewsApp.Api.Extensions;

internal static partial class LoggerExtensions
{
    [LoggerMessage(LogLevel.Error, "An exception occurred while fetching articles")]
    public static partial void GetAllArticlesQueryHandlerError(this ILogger logger, Exception exception);
}
