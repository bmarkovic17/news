using System.Threading;
using System.Threading.Tasks;

namespace NewsApp.Core.SharedKernel;

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    Task<ResultBase> HandleAsync(TCommand command, CancellationToken cancellationToken);
}
