using Decidas.Abstractions.Events;

namespace Decidas.Groups.Domain.Events;

public record GroupCreatedEvent(Guid Id, string Name) : IDomainEvent;