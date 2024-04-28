using Decidas.Areas.Groups.Models;
using Decidas.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Decidas.Areas.Groups.Features;

public record struct GetGroupRequest(Guid Id);

public record struct GroupDetails(Guid Id, string Name, DateTime StartDate);

public class GetGroupQuery(ILogger<GetGroupQuery> _logger, ApplicationDb _db)
{
    public async Task<GroupDetails?> ExecuteAsync(GetGroupRequest request, CancellationToken cancel)
    {
        _logger.LogInformation("Executing GetGroup query for {groupId}", request.Id);

        var groupId = new GroupId(request.Id);

        var group = await _db.Groups.FirstOrDefaultAsync(group => group.Id == groupId, cancel);

        return group is not null
            ? new GroupDetails(
                group.Id.Value,
                group.Name,
                group.StartDate.Value.ToDateTime(TimeOnly.MinValue))
            : null;
    }
}

[ApiController]
[Route("api/groups")]
public class GetGroupEndpoint(ILogger<GetGroupEndpoint> _logger, GetGroupQuery _query) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GroupDetails>> HandleAsync(Guid id, CancellationToken cancel)
    {
        _logger.LogInformation("Handling GetGroup request for {id}", id);

        var request = new GetGroupRequest(id);

        var response = await _query.ExecuteAsync(request, cancel);

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }
}
