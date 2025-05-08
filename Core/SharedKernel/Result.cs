using System;
using System.Collections.Generic;
using System.Linq;
 using NewsApp.Core.Extensions;

namespace NewsApp.Core.SharedKernel;

public abstract class ResultBase
{
    private protected ResultBase(IDictionary<string, ICollection<string>>? errors = null) =>
        Errors = errors?.ToDictionary(
            entry => entry.Key,
            entry => entry.Value) ?? [];

    public bool IsSuccessful =>
        Errors.IsNullOrEmpty();

    public Dictionary<string, ICollection<string>> Errors { get; }
}

public sealed class Result(IDictionary<string, ICollection<string>>? errors = null) : ResultBase(errors)
{
    private static readonly Result SuccessInstance = new();

    public static Result Success() =>
        SuccessInstance;

    public static Result Fail(params IEnumerable<IDictionary<string, ICollection<string>>> errors)
    {
        var processedErrors = ResultHelper.ProcessErrors(errors);

        ResultHelper.EnsureAtLeastOneError(processedErrors);

        return new Result(processedErrors);
    }

    public static Result Create(params IEnumerable<IDictionary<string, ICollection<string>>> errors)
    {
        var processedErrors = ResultHelper.ProcessErrors(errors);

        return processedErrors.IsNullOrEmpty()
            ? Success()
            : Fail(processedErrors);
    }
}

public sealed class Result<T> : ResultBase
{
    private Result(T? value = default, IDictionary<string, ICollection<string>>? errors = null) : base(errors) =>
        Value = value;

    public T? Value { get; }

    public static Result<T> Success(T value) =>
        new(value);

    public static Result<T> Fail(params IEnumerable<IDictionary<string, ICollection<string>>> errors)
    {
        var processedErrors = ResultHelper.ProcessErrors(errors);

        ResultHelper.EnsureAtLeastOneError(processedErrors);

        return new Result<T>(errors: processedErrors);
    }

    public static Result<T> Create(Func<T> valueFactory, params IEnumerable<IDictionary<string, ICollection<string>>> errors)
    {
        var processedErrors = ResultHelper.ProcessErrors(errors);

        return processedErrors.IsNullOrEmpty()
            ? Success(valueFactory())
            : Fail(processedErrors);
    }
}

public sealed class PagedResult<T> : ResultBase
{
    private PagedResult(T? value = default, int? page = null, int? size = null, IDictionary<string, ICollection<string>>? errors = null) : base(errors)
    {
        Value = value;
        Page = page;
        Size = size;
    }

    public T? Value { get; }

    public int? Page { get; }

    public int? Size { get; }

    public static PagedResult<T> Success(T value, int page, int size) =>
        new(value, page, size);

    public static PagedResult<T> Fail(params IEnumerable<IDictionary<string, ICollection<string>>> errors)
    {
        var processedErrors = ResultHelper.ProcessErrors(errors);

        ResultHelper.EnsureAtLeastOneError(processedErrors);

        return new PagedResult<T>(errors: processedErrors);
    }

    public static PagedResult<T> Create(Func<T> valueFactory, int page, int size, params IEnumerable<IDictionary<string, ICollection<string>>> errors)
    {
        var processedErrors = ResultHelper.ProcessErrors(errors);

        return processedErrors.IsNullOrEmpty()
            ? Success(valueFactory(), page, size)
            : Fail(processedErrors);
    }
}

file static class ResultHelper
{
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

    public static void EnsureAtLeastOneError(IDictionary<string, ICollection<string>> errors)
    {
        var errorCount = errors.Aggregate(0, (current, error) => current + error.Value.Count);

        if (errorCount > 0)
            return;

        errors.Clear();
        errors.Add(string.Empty, ["error"]);
    }
}
