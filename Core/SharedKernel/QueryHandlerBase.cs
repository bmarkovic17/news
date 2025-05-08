using System.Threading.Tasks;

namespace NewsApp.Core.SharedKernel;

/// <summary>
/// Base class for all query handlers in the CQRS pattern.
/// Provides common validation and execution flow for queries.
/// </summary>
/// <typeparam name="TQuery">The type of query to handle.</typeparam>
public abstract class QueryHandlerBase<TQuery> where TQuery : IQuery
{
    /// <summary>
    /// Handles the specified query asynchronously, including validation.
    /// </summary>
    /// <param name="query">The query to handle.</param>
    /// <returns>A task representing the asynchronous operation, with a result containing the query data or validation errors.</returns>
    public async Task<ResultBase> HandleAsync(TQuery query)
    {
        var validateResult = query.Validate();

        if (validateResult.IsSuccessful is false)
            return validateResult;

        var result = await RunAsync(query);

        return result;
    }

    /// <summary>
    /// Executes the query logic after validation has passed.
    /// Must be implemented by derived classes.
    /// </summary>
    /// <param name="query">The validated query to execute.</param>
    /// <returns>A task representing the asynchronous operation, with a result containing the query data.</returns>
    protected abstract Task<ResultBase> RunAsync(TQuery query);
}
