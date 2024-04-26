using Decidas.Areas.Groups.Features;
using Decidas.Areas.Groups.Models;
using Microsoft.AspNetCore.Mvc;

namespace Decidas.Areas.Groups;

[ApiController]
[Route("api/v1/groups")]
public class GroupsEndpoints(ILogger<GroupsEndpoints> logger, CreateGroupCommand command) : ControllerBase
{
    private readonly ILogger<GroupsEndpoints> _logger = logger;
    private readonly CreateGroupCommand _command = command;

    [HttpPost]
    public async Task<ActionResult<CreateGroupResponse>> Create([FromBody]CreateGroupRequest request)
    {
        var response = await _command.ProcessAsync(request);

        return Created();
    }
}
