using Decidas.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Decidas.Areas.Structure.Features;

public record AssignKeeperRequest(Guid GroupId, Guid KeeperId, DateTime AssignDate);

public class AssignKeeperCommand(ILogger<AssignKeeperCommand> _logger, ApplicationDb _db)
{
    public async Task ExecuteAsync(AssignKeeperRequest request, CancellationToken cancel)
    {
        _logger.LogInformation("Assigning keeper {keeperId} to group {groupId}", request.KeeperId, request.GroupId);

        var group = await _db.Groups.Include(g => g.Assignments)
            .FirstOrDefaultAsync(g => g.Id.Value == request.GroupId, cancel)
            ?? throw new UnknownGroupForAssigningKeeper(request.GroupId);

        group.AssignKeeper(new(request.KeeperId), DateOnly.FromDateTime(request.AssignDate));

        await _db.SaveChangesAsync(cancel);
    }
}

[ApiController]
[Route("api/structure/groups/assign-keeper")]
public class AssignKeeperEndpoint(ILogger<AssignKeeperEndpoint> _logger, AssignKeeperCommand _command) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> HandleAsync([FromBody]AssignKeeperRequest request, CancellationToken cancel)
    {
        _logger.LogInformation("Handling AssignKeeper request for group {groupId}", request.GroupId);

        await _command.ExecuteAsync(request, cancel);

        return Created();
    }
}

public class UnknownGroupForAssigningKeeper : DomainError
{
    public UnknownGroupForAssigningKeeper(Guid groupId)
    {
        Details = $"Unknown group {groupId} for assigning keeper.";
    }
}
