using System;
using Microsoft.Extensions.Logging;

namespace NewsApp.Api.Extensions;

/// <summary>
/// Provides extension methods for logging application-specific events.
/// Uses source generation for high-performance logging with structured data.
/// </summary>
internal static partial class LoggerExtensions
{
    /// <summary>
    /// Logs an error that occurred during article retrieval.
    /// </summary>
    /// <param name="logger">The logger instance to use.</param>
    /// <param name="exception">The exception that was thrown during article retrieval.</param>
    [LoggerMessage(LogLevel.Error, "An exception occurred while fetching articles")]
    public static partial void GetAllArticlesQueryError(this ILogger logger, Exception exception);

    /// <summary>
    /// Logs an error that occurred during article creation.
    /// </summary>
    /// <param name="logger">The logger instance to use.</param>
    /// <param name="exception">The exception that was thrown during article creation.</param>
    [LoggerMessage(LogLevel.Error, "An exception occurred while creating a new article")]
    public static partial void CreateArticleCommandError(this ILogger logger, Exception exception);
}
