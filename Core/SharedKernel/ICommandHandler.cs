using System.Threading;
using System.Threading.Tasks;

namespace NewsApp.Core.SharedKernel;

/// <summary>
/// Defines a handler for a specific command type in the CQRS pattern.
/// </summary>
/// <typeparam name="TCommand">The type of command to handle.</typeparam>
public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    /// <summary>
    /// Handles the specified command asynchronously.
    /// </summary>
    /// <param name="command">The command to handle.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, with a result indicating success or failure.</returns>
    Task<ResultBase> HandleAsync(TCommand command, CancellationToken cancellationToken);
}
