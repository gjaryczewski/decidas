using System.Threading;
using System.Threading.Tasks;

namespace Decidas.Abstractions.Commands;

public interface ICommandHandler<in TCommand, TResult>
{
    Task<TResult> Handle(TCommand command, CancellationToken cancellation);
}