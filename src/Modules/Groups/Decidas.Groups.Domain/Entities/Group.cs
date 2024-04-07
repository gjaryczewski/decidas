using Decidas.Abstractions.Entities;
using Decidas.Groups.Domain.Events;

namespace Decidas.Groups.Domain.Entities;

public sealed class Group : Entity
{
    public string Name { get; private set; } = null!;

    public DateOnly StartDate { get; private set; }

    public Group(
        string name,
        DateOnly startDate,
        Guid? id = null)
    : base(id)
    {
        Name = name;
        StartDate = startDate;
    }

    public void Rename(string name)
    {
        if (Name == name)
        {
            return;
        }

        Name = name;

        _domainEvents.Add(new GroupRenamedEvent(Id, Name));
    }
}
