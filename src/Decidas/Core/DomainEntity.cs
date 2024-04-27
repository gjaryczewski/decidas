namespace Decidas.Core;

public class DomainEntity
{
    protected readonly List<DomainEvent> _domainEvents = [];

    public void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
