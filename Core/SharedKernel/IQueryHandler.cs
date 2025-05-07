using System.Threading.Tasks;

namespace NewsApp.Core.SharedKernel;

public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery
{
    Task<Result<TResult>> HandleAsync(TQuery query);
}
