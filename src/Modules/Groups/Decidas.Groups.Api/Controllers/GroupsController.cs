using Decidas.Abstractions;
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
    public async Task<ActionResult<GroupDto>> Get(Guid GroupId)
    {
        // var Group = await _dispatcher.QueryAsync(new GetGroup { GroupId = GroupId });
        // if (Group is not null)
        // {
        //     return Ok(Group);
        // }

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