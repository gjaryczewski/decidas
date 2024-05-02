using Decidas.Areas.People.Models;
using Decidas.Core;
using Decidas.Shared;

namespace Decidas.Areas.Structure.Models;

public class Group : DomainEntity
{
    public GroupId Id { get; private set; } = default!;

    public string Name { get; set; } = default!;

    public GroupStartDate StartDate { get; private set; } = default!;

    public List<Assignment> Assignments { get; private set; } = default!;

    public Group() {}

    public static Group Create(string name, DateOnly startDate)
    {
        var group = new Group
        {
            Id = new(Guid.NewGuid()),
            Name = name,
            StartDate = new(startDate)
        };

        group.AddDomainEvent(new GroupCreated(group.Id.Value));

        return group;
    }

    public void AssignKeeper(KeeperId keeperId, DateOnly assignDate)
    {
        Assignments.Add(new Assignment(Id, keeperId, assignDate));

        AddDomainEvent(new KeeperAssignedToGroup(Id.Value, keeperId.Value));
    }
}

public record GroupId(Guid Value);

public record GroupStartDate
{
    public DateOnly Value { get; init; }

    public static readonly DateOnly Oldest = new(2018, 1, 1);

    public GroupStartDate(DateOnly value)
    {
        if (value < Oldest)
        {
            throw new TooOldStartDate(value);
        }

        Value = value;
    }
}

public record GroupType(Guid Id, string Name, DateTime StartDate)
{
    public static GroupType FromGroup(Group group) => new(
        group.Id.Value,
        group.Name,
        group.StartDate.Value.ToDateTime()
    );
}

public class TooOldStartDate : DomainError
{
    public TooOldStartDate(DateOnly startDate)
    {
        Details = $"Start date {startDate} is earlier than oldest possible {GroupStartDate.Oldest}.";
    }
}

public class GroupCreated(Guid id) : DomainEvent(id) {}

public class KeeperAssignedToGroup(Guid groupId, Guid keeperId) : DomainEvent(null)
{
    public Guid GroupId { get; }= groupId;

    public Guid Keeper { get; }= keeperId;
}
