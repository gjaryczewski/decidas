using Decidas.Abstractions.Commands;
using Decidas.Groups.Api.Models;
using Decidas.Groups.Contracts.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Decidas.Groups.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupsController(ICommandDispatcher commandDispatcher) : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher = commandDispatcher;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(CreateGroupRequest request, CancellationToken cancellation)
    {
        var command = new CreateGroupCommand(request.Name, request.StartDate);
        var result = await _commandDispatcher.SendAsync<CreateGroupCommand, Guid>(command, cancellation);

        // TODO Replace "Get" with nameof(Get).
        return CreatedAtAction("Get", new { Id = result });
    }
}