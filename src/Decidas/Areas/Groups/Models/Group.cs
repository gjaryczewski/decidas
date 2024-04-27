using Decidas.Core;

namespace Decidas.Areas.Groups.Models;

public class Group : DomainEntity
{
    public GroupId Id { get; private set; }

    public required string Name { get; set; }

    public GroupStartDate StartDate { get; private set; }

    public Group() {}

    public static Group Create(string name, DateOnly startDate)
    {
        var group = new Group
        {
            Id = new(Guid.NewGuid()),
            Name = name,
            StartDate = new(startDate)
        };

        group.AddDomainEvent(new GroupCreatedEvent(group.Id.Value));

        return group;
    }
}

public readonly record struct GroupId(Guid Value);

public readonly record struct GroupStartDate
{
    public DateOnly Value { get; init; }

    public static readonly DateOnly Oldest = new(2018, 1, 1);

    public GroupStartDate(DateOnly value)
    {
        if (value < Oldest)
        {
            throw new TooOldStartDateError(value);
        }

        Value = value;
    }
}

public class TooOldStartDateError : DomainError
{
    public TooOldStartDateError(DateOnly startDate)
    {
        Details = $"Start date {startDate} is earlier than oldest possible {GroupStartDate.Oldest}.";
    }
}

public class GroupCreatedEvent(Guid id) : DomainEvent(id) {}
