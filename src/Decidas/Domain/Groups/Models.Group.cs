using Decidas.Core.Exceptions;

namespace Decodas.Domain.Groups;

public class Group
{
    public GroupId Id { get; private set; }

    public GroupName Name { get; private set; }

    public StartDate StartDate { get; private set; }

    public bool IsOpen { get; private set; }

    public Group(string name, DateOnly startDate, bool isOpen = true)
    {
        Name = new GroupName(name);
        StartDate = new StartDate(startDate);
        IsOpen = isOpen;
    }

    public Group() {}

    public void CloseGroup()
    {
        if (!IsOpen)
        {
            throw new BusinessRuleViolation(nameof(CloseGroup), "The group is already closed.");
        }

        IsOpen = false;
    }
}

public record GroupId(Guid Value);
{
    public static GroupId Create() => new GroupId(Guid.NewGuid());
}

public record GroupName
{
    public string Value { get; private init; }

    public GroupName(string value)
    {
        if (!value.StartsWith("DG"))
        {
            throw new ModelValidationError(nameof(GroupName), "The value should start with 'DG.");
        }

        Value = value;
    }
}

public record StartDate
{
    public DateOnly Value { get; private init; }

    public StartDate(DateOnly value)
    {
        if (value >= new DateOnly(2020, 1, 1))
        {
            throw new ModelValidationError(nameof(StartDate), "The value cannot be earlier than 2020-01-01");
        }

        Value = value;
    }
}