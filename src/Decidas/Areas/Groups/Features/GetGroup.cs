using Decidas.Areas.Groups.Models;
using Decidas.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Decidas.Areas.Groups.Features;

public record struct GetGroupRequest(Guid Id);

public record struct GetGroupResponse(Guid Id, string Name, DateTime StartDate);

public class GetGroupQuery(ILogger<GetGroupQuery> _logger, ApplicationDb _db)
{
    public async Task<GetGroupResponse?> ProcessAsync(GetGroupRequest request, CancellationToken cancel)
    {
        _logger.LogInformation("Processing GetGroup query for {groupId}", request.Id);

        var groupId = new GroupId(request.Id);

        var group = await _db.Groups.FirstAsync(group => group.Id == groupId, cancel);

        return group is not null
            ? new GetGroupResponse(
                group.Id.Value,
                group.Name,
                group.StartDate.Value.ToDateTime(TimeOnly.MinValue))
            : null;
    }
}

[ApiController]
[Route("api/groups")]
public class GetGroupEndpoint(GetGroupQuery _query) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetGroupResponse>> HandleAsync(Guid id, CancellationToken cancel)
    {
        var request = new GetGroupRequest(id);

        var response = await _query.ProcessAsync(request, cancel);

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }
}
