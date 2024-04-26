namespace Decidas.Core;

public sealed class DomainEventCollector
{
    private List<DomainEvent> _events = new();

    public void Collect(DomainEvent domainEvent) => _events.Add(domainEvent);
}
