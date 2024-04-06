using Decidas.Groups.Contracts.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Decidas.Abstractions.Commands;

namespace Decidas.Groups.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupsController : ControllerBase
{
    private readonly ICommandDispatcher _commands;

    public GroupsController(ICommandDispatcher commands)
    {
        _commands = commands;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(CreateGroupCommand command, CancellationToken cancellationToken)
    {
        await _commands.Dispatch<CreateGroupCommand, Guid>(command, cancellationToken);

        return CreatedAtAction("Get", new { GroupId = command.GroupId }, null);
    }
}