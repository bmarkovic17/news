namespace NewsApp.Core.SharedKernel;

/// <summary>
/// Defines the contract for query objects in the CQRS pattern.
/// Queries represent read operations that do not change the state of the system.
/// </summary>
public interface IQuery
{
    /// <summary>
    /// Validates the query parameters before execution.
    /// </summary>
    /// <returns>A result indicating whether the query is valid for execution.</returns>
    ResultBase Validate();
}
