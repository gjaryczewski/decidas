namespace Decidas.Abstractions.Events;

public interface IEventHandler<TEvent>
{
    Task HandleAsync(TEvent @event, CancellationToken cancellation);
}