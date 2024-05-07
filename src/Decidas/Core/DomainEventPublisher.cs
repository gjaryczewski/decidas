namespace Decidas.Core;

public class DomainEventPublisher
{
    protected readonly List<DomainEvent> _domainEvents = [];

    public void PublishDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
