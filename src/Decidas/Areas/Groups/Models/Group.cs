namespace Decidas.Areas.Groups.Models;

internal readonly record struct GroupId(Guid Value);

internal sealed class Group
{
    public GroupId Id { get; private set; }

    public required string Name { get; set; }

    public DateOnly StartDate { get; private set; }

    public Group() {}

    public static Group Create(string name, DateOnly startDate)
    {
        return new()
        {
            Id = new(Guid.NewGuid()),
            Name = name,
            StartDate = startDate
        };
    }
}
