using Decidas.Areas.Groups.Models;
using Decidas.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Decidas.Areas.Groups.Features;

public record struct GetGroupDetailsRequest(Guid Id);

public record struct GroupDetails(Guid Id, string Name, DateTime StartDate);

public class GetGroupDetailsQuery(ILogger<GetGroupDetailsQuery> _logger, ApplicationDb _db)
{
    public async Task<GroupDetails?> ExecuteAsync(GetGroupDetailsRequest request, CancellationToken cancel)
    {
        _logger.LogInformation("Executing GetGroupDetails query for group ID {groupId}", request.Id);

        var groupId = new GroupId(request.Id);

        var group = await _db.Groups.AsNoTracking().FirstOrDefaultAsync(group => group.Id == groupId, cancel);

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
public class GetGroupDetailsEndpoint(ILogger<GetGroupDetailsEndpoint> _logger, GetGroupDetailsQuery _query) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GroupDetails>> HandleAsync(Guid id, CancellationToken cancel)
    {
        _logger.LogInformation("Handling GetGroupDetails request for group ID {id}", id);

        var request = new GetGroupDetailsRequest(id);

        var response = await _query.ExecuteAsync(request, cancel);

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }
}
