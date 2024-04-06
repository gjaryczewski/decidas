using Decidas.Abstractions;
using Decidas.Groups.Contracts.Commands;
using Decidas.Groups.Contracts.Queries;
using Decidas.Groups.Contracts.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Decidas.Groups.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupsController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public GroupsController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet("{groupid:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetGroupResponse>> Get(Guid groupId)
    {
        var response = await _dispatcher.QueryAsync(new GetGroupQuery(groupId));
        if (response is not null)
        {
            return Ok(response);
        }

        return NotFound();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(CreateGroupCommand command)
    {
        await _dispatcher.SendAsync(command);

        return CreatedAtAction(nameof(Get), new { GroupId = command.GroupId }, null);
    }
}