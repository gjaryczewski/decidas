using System.Threading;
using System.Threading.Tasks;

namespace Decidas.Abstractions;

public interface IDispatcher
{
    Task SendAsync<T>(T command, CancellationToken cancellationToken = default) where T : class, ICommand;
    Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, IEvent;
    Task<T> QueryAsync<T>(IQuery<T> query, CancellationToken cancellationToken = default);
}
