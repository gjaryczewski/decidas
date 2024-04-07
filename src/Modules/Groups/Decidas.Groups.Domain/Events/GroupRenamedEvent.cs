using Decidas.Abstractions.Events;

namespace Decidas.Groups.Domain.Events;

public record GroupRenamedEvent(Guid Id, string Name) : IDomainEvent;