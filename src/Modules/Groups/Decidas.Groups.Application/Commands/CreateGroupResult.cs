using Decidas.Groups.Domain.Entities;

namespace Decidas.Groups.Contracts.Commands;

public record CreateGroupResult(Guid Id)
{
    public static CreateGroupResult From(Group group)
    {
        return new CreateGroupResult(group.Id);
    }
}