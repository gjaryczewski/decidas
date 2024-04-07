using Decidas.Abstractions.Events;
using Decidas.Groups.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Decidas.Groups.Application.Events;

public class GroupCreatedHandler(ILogger<GroupCreatedHandler> logger) : IEventHandler<GroupCreatedEvent>
{
    private readonly ILogger<GroupCreatedHandler> _logger = logger;

    public Task HandleAsync(GroupCreatedEvent @event, CancellationToken cancellation)
    {
        _logger.LogTrace($"{0} = {1}", nameof(GroupCreatedEvent), @event.Id);

        return Task.CompletedTask;
    }
}