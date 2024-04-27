namespace Decidas.Core;

public class DomainEvent(Guid id)
{
    public Guid Id { get; } = id;
}
