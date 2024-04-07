namespace Decidas.Groups.Contracts.Commands;

public record CreateGroupCommand(string Name, DateOnly StartDate);