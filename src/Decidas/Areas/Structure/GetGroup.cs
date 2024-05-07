using Decidas.Areas.Structure.Models;
using Decidas.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Decidas.Areas.Structure;

public record GetGroupRequest(Guid Id);

public class GetGroupQuery(ILogger<GetGroupQuery> _logger, ApplicationDb _db)
{
    public async Task<GroupType?> ExecuteAsync(GetGroupRequest request, CancellationToken cancel)
    {
        _logger.LogInformation("Executing GetGroup query for group ID {groupId}", request.Id);

        var groupId = new GroupId(request.Id);
        var group = await _db.Groups.AsNoTracking()
            .FirstOrDefaultAsync(group => group.Id == groupId, cancel);

        return group is not null ? GroupType.FromGroup(group) : null;
    }
}

[ApiController]
[Route("api/structure/groups")]
public class GetGroupEndpoint(ILogger<GetGroupEndpoint> _logger, GetGroupQuery _query) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Group>> HandleAsync(Guid id, CancellationToken cancel)
    {
        _logger.LogInformation("Handling GetGroup request for group ID {id}", id);

        var request = new GetGroupRequest(id);

        var response = await _query.ExecuteAsync(request, cancel);

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }
}
