using Decidas.Abstractions;

namespace Decidas.Groups.Events;

public record GroupCreatedEvent(Guid GroupId, string GroupName) : IEvent;