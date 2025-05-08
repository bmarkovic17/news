using System.Threading.Tasks;

namespace NewsApp.Core.SharedKernel;

public abstract class QueryHandlerBase<TQuery> where TQuery : IQuery
{
    public async Task<ResultBase> HandleAsync(TQuery query)
    {
        var validateResult = query.Validate();

        if (validateResult.IsSuccessful is false)
            return validateResult;

        var result = await RunAsync(query);

        return result;
    }

    protected abstract Task<ResultBase> RunAsync(TQuery query);
}
