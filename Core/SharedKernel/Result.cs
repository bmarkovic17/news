using System;
using System.Collections.Generic;
using System.Linq;
 using NewsApp.Core.Extensions;

namespace NewsApp.Core.SharedKernel;

public sealed class Result
{
    private static readonly Result SuccessInstance = Success();

    private Result(IDictionary<string, ICollection<string>>? errors = null) =>
        Errors = errors?.ToDictionary(
            entry => entry.Key,
            entry => entry.Value) ?? [];

    public bool IsSuccessful =>
        Errors.IsNullOrEmpty();

    public Dictionary<string, ICollection<string>> Errors { get; }

    public static Result Success() =>
        SuccessInstance;

    public static Result Fail(IDictionary<string, ICollection<string>> errors) =>
        new(errors);

    public static Result Create(params IEnumerable<IDictionary<string, ICollection<string>>> errors)
    {
        var processedErrors = ResultHelper.ProcessErrors(errors);

        return processedErrors.IsNullOrEmpty()
            ? Success()
            : Fail(processedErrors);
    }
}

public sealed class Result<T>
{
    private Result(T? value, IDictionary<string, ICollection<string>>? errors = null)
    {
        Value = value;
        Errors = errors?.ToDictionary(
            entry => entry.Key,
            entry => entry.Value) ?? [];
    }

    public T? Value { get; }

    public bool IsSuccessful =>
        Errors.IsNullOrEmpty();

    public Dictionary<string, ICollection<string>> Errors { get; }

    public static Result<T> Success(T value) =>
        new(value);

    public static Result<T> Fail(IDictionary<string, ICollection<string>> errors) =>
        new(value: default, errors);

    public static Result<T> Create(Func<T> valueFactory, params IEnumerable<IDictionary<string, ICollection<string>>> errors)
    {
        var processedErrors = ResultHelper.ProcessErrors(errors);

        return processedErrors.IsNullOrEmpty()
            ? Success(valueFactory())
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
}
