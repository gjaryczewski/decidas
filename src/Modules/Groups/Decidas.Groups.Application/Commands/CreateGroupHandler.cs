using Decidas.Abstractions.Commands;

namespace Decidas.Groups.Application.Commands;

public class CreateGroupHandler<CreateGroupCommand, CreateGroupResult> : ICommandHandler<CreateGroupCommand, CreateGroupResult>
{
    public async Task<CreateGroupResult> Handle(CreateGroupCommand command, CancellationToken cancellation)
    {
        CreateGroupResult result = default;

        return await Task.FromResult<CreateGroupResult>(result);
    }
}