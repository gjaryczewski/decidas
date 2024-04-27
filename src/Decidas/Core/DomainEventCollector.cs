namespace Decidas.Core;

public class DomainEventCollector
{
    private List<DomainEvent> _events = new();

    public void Collect(DomainEvent domainEvent) => _events.Add(domainEvent);
}
