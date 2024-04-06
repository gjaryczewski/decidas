using System.Threading;
using System.Threading.Tasks;

namespace Decidas.Abstractions.Commands;

public interface ICommandDispatcher
{
    Task<TResult> Dispatch<TCommand, TResult>(TCommand command, CancellationToken cancellation);
}