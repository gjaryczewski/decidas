using Decidas.Abstractions;

namespace Decidas.Groups.Contracts.Commands;

public record CreateGroupCommand(string GroupName) : ICommand
{
    public Guid GroupId { get; init; } = Guid.NewGuid();
}