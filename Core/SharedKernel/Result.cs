using System;
using System.Collections.Generic;
using System.Linq;
 using NewsApp.Core.Extensions;

namespace NewsApp.Core.SharedKernel;

/// <summary>
/// Base class for all result types in the application.
/// Provides common functionality for handling operation results.
/// </summary>
public abstract class ResultBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResultBase"/> class.
    /// </summary>
    /// <param name="errors">Dictionary of errors, where the key is the error field and the value is a collection of error codes.</param>
    private protected ResultBase(IDictionary<string, ICollection<string>>? errors = null) =>
        Errors = errors?.ToDictionary(
            entry => entry.Key,
            entry => entry.Value) ?? [];

    /// <summary>
    /// Gets a value indicating whether the operation was successful (contains no errors).
    /// </summary>
    public bool IsSuccessful =>
        Errors.IsNullOrEmpty();

    /// <summary>
    /// Gets the dictionary of errors associated with the result.
    /// Keys represent error fields, values are collections of error codes.
    /// </summary>
    public Dictionary<string, ICollection<string>> Errors { get; }
}

/// <summary>
/// Represents the result of an operation that does not return a value.
/// Used to indicate success or failure of an operation.
/// </summary>
/// <param name="errors">Dictionary of errors, where the key is the error field and the value is a collection of error codes.</param>
public sealed class Result(IDictionary<string, ICollection<string>>? errors = null) : ResultBase(errors)
{
    private static readonly Result SuccessInstance = new();

    /// <summary>
    /// Creates a successful result with no value.
    /// </summary>
    /// <returns>A successful result instance.</returns>
    public static Result Success() =>
        SuccessInstance;

    /// <summary>
    /// Creates a failed result with the specified errors.
    /// </summary>
    /// <param name="errors">Collections of errors to include in the result.</param>
    /// <returns>A failed result containing the specified errors.</returns>
    public static Result Fail(params IEnumerable<IDictionary<string, ICollection<string>>> errors)
    {
        var processedErrors = ResultHelper.ProcessErrors(errors);

        ResultHelper.EnsureAtLeastOneError(processedErrors);

        return new Result(processedErrors);
    }

    /// <summary>
    /// Creates a result based on the presence of errors.
    /// </summary>
    /// <param name="errors">Collections of errors to check and potentially include in the result.</param>
    /// <returns>A successful result if no errors are present; otherwise, a failed result with the specified errors.</returns>
    public static Result Create(params IEnumerable<IDictionary<string, ICollection<string>>> errors)
    {
        var processedErrors = ResultHelper.ProcessErrors(errors);

        return processedErrors.IsNullOrEmpty()
            ? Success()
            : Fail(processedErrors);
    }
}

/// <summary>
/// Represents the result of an operation that returns a value of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the value returned by the operation.</typeparam>
public sealed class Result<T> : ResultBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class.
    /// </summary>
    /// <param name="value">The value returned by the operation.</param>
    /// <param name="errors">Dictionary of errors, where the key is the error field and the value is a collection of error codes.</param>
    private Result(T? value = default, IDictionary<string, ICollection<string>>? errors = null) : base(errors) =>
        Value = value;

    /// <summary>
    /// Gets the value returned by the operation.
    /// Will be default(T) if the operation failed.
    /// </summary>
    public T? Value { get; }

    /// <summary>
    /// Creates a successful result with the specified value.
    /// </summary>
    /// <param name="value">The value to include in the result.</param>
    /// <returns>A successful result containing the specified value.</returns>
    public static Result<T> Success(T value) =>
        new(value);

    /// <summary>
    /// Creates a failed result with the specified errors.
    /// </summary>
    /// <param name="errors">Collections of errors to include in the result.</param>
    /// <returns>A failed result containing the specified errors.</returns>
    public static Result<T> Fail(params IEnumerable<IDictionary<string, ICollection<string>>> errors)
    {
        var processedErrors = ResultHelper.ProcessErrors(errors);

        ResultHelper.EnsureAtLeastOneError(processedErrors);

        return new Result<T>(errors: processedErrors);
    }

    /// <summary>
    /// Creates a result based on the presence of errors.
    /// </summary>
    /// <param name="valueFactory">A function that creates the value to include in the result if no errors are present.</param>
    /// <param name="errors">Collections of errors to check and potentially include in the result.</param>
    /// <returns>A successful result with the value from valueFactory if no errors are present; otherwise, a failed result with the specified errors.</returns>
    public static Result<T> Create(Func<T> valueFactory, params IEnumerable<IDictionary<string, ICollection<string>>> errors)
    {
        var processedErrors = ResultHelper.ProcessErrors(errors);

        return processedErrors.IsNullOrEmpty()
            ? Success(valueFactory())
            : Fail(processedErrors);
    }
}

/// <summary>
/// Represents the result of an operation that returns a paginated collection of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the paginated collection returned by the operation.</typeparam>
public sealed class PagedResult<T> : ResultBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PagedResult{T}"/> class.
    /// </summary>
    /// <param name="value">The paginated collection returned by the operation.</param>
    /// <param name="page">The current page number.</param>
    /// <param name="size">The page size.</param>
    /// <param name="errors">Dictionary of errors, where the key is the error field and the value is a collection of error codes.</param>
    private PagedResult(T? value = default, int? page = null, int? size = null, IDictionary<string, ICollection<string>>? errors = null) : base(errors)
    {
        Value = value;
        Page = page;
        Size = size;
    }

    /// <summary>
    /// Gets the paginated collection returned by the operation.
    /// Will be default(T) if the operation failed.
    /// </summary>
    public T? Value { get; }

    /// <summary>
    /// Gets the current page number.
    /// </summary>
    public int? Page { get; }

    /// <summary>
    /// Gets the page size.
    /// </summary>
    public int? Size { get; }

    /// <summary>
    /// Creates a successful paged result with the specified value, page, and size.
    /// </summary>
    /// <param name="value">The value to include in the result.</param>
    /// <param name="page">The current page number.</param>
    /// <param name="size">The page size.</param>
    /// <returns>A successful paged result containing the specified value, page, and size.</returns>
    public static PagedResult<T> Success(T value, int page, int size) =>
        new(value, page, size);

    /// <summary>
    /// Creates a failed paged result with the specified errors.
    /// </summary>
    /// <param name="errors">Collections of errors to include in the result.</param>
    /// <returns>A failed paged result containing the specified errors.</returns>
    public static PagedResult<T> Fail(params IEnumerable<IDictionary<string, ICollection<string>>> errors)
    {
        var processedErrors = ResultHelper.ProcessErrors(errors);

        ResultHelper.EnsureAtLeastOneError(processedErrors);

        return new PagedResult<T>(errors: processedErrors);
    }

    /// <summary>
    /// Creates a paged result based on the presence of errors.
    /// </summary>
    /// <param name="valueFactory">A function that creates the value to include in the result if no errors are present.</param>
    /// <param name="page">The current page number.</param>
    /// <param name="size">The page size.</param>
    /// <param name="errors">Collections of errors to check and potentially include in the result.</param>
    /// <returns>A successful paged result with the value from valueFactory, page, and size if no errors are present; otherwise, a failed paged result with the specified errors.</returns>
    public static PagedResult<T> Create(Func<T> valueFactory, int page, int size, params IEnumerable<IDictionary<string, ICollection<string>>> errors)
    {
        var processedErrors = ResultHelper.ProcessErrors(errors);

        return processedErrors.IsNullOrEmpty()
            ? Success(valueFactory(), page, size)
            : Fail(processedErrors);
    }
}

/// <summary>
/// Helper class for processing and managing errors in result objects.
/// </summary>
file static class ResultHelper
{
    /// <summary>
    /// Processes multiple collections of errors into a single dictionary.
    /// Deduplicates error codes and maintains case-insensitive comparison.
    /// </summary>
    /// <param name="errors">Collections of errors to process.</param>
    /// <returns>A dictionary containing all unique errors from the input collections.</returns>
    public static Dictionary<string, ICollection<string>> ProcessErrors(params IEnumerable<IDictionary<string, ICollection<string>>> errors)
    {
        var processedErrors = new Dictionary<string, ICollection<string>>();

        foreach (var error in errors)
        {
            foreach (var (errorKey, errorCodes) in error)
            {
                if (errorCodes.Count == 0)
                    continue;

                _ = processedErrors.TryAdd(errorKey, []);

                foreach (var errorCode in errorCodes)
                    if (processedErrors[errorKey].Contains(errorCode, StringComparer.OrdinalIgnoreCase) is false)
                        processedErrors[errorKey].Add(errorCode);
            }
        }

        return processedErrors;
    }

    /// <summary>
    /// Ensures that an error dictionary contains at least one error.
    /// If the dictionary is empty, adds a default error.
    /// </summary>
    /// <param name="errors">The dictionary of errors to check and potentially modify.</param>
    public static void EnsureAtLeastOneError(IDictionary<string, ICollection<string>> errors)
    {
        var errorCount = errors.Aggregate(0, (current, error) => current + error.Value.Count);

        if (errorCount > 0)
            return;

        errors.Clear();
        errors.Add(string.Empty, ["error"]);
    }
}
