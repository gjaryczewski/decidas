using Decidas.Abstractions.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Decidas.Infrastructure.Events;

public class EventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider _services;

    public EventDispatcher(IServiceProvider services) => _services = services;

    public Task PublishAsync<TEvent>(TEvent Event, CancellationToken cancellation)
    {
        var handler = _services.GetRequiredService<IEventHandler<TEvent>>();

        return handler.HandleAsync(Event, cancellation);
    }
}