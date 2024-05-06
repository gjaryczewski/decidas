namespace Decidas.Areas.Structure.Models;

public class Assignment
{
    public AssignmentId Id { get; private set; } = default!;

    public GroupId GroupId { get; private set; } = default!;

    public KeeperId KeeperId { get; private set; } = default!;

    public DateOnly AssignDate { get; private set; } = default!;

    public static Assignment Create(GroupId groupId, KeeperId keeperId, DateOnly assignDate) => new()
    {
        Id = new(Guid.NewGuid()),
        GroupId = groupId,
        KeeperId = keeperId,
        AssignDate = assignDate
    };
}

public record AssignmentId(Guid Value);

public record KeeperId(Guid Value);
