using Decidas.Abstractions.Events;

namespace Decidas.Abstractions.Entities;

public abstract class Entity
{
    public Guid Id { get; private init; }

    protected readonly List<IEvent> _domainEvents = [];

    protected Entity(Guid? id)
    {
        Id = id ?? Guid.NewGuid();
    }

    public List<IEvent> PopDomainEvents()
    {
        var copy = _domainEvents.ToList();
        _domainEvents.Clear();

        return copy;
    }

    protected Entity() { }
}