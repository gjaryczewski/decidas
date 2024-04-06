namespace Decidas.Groups.Contracts.Commands;

public record CreateGroupCommand(string GroupName, DateOnly StartDate)
{
    public Guid GroupId { get; init; } = Guid.NewGuid();
}