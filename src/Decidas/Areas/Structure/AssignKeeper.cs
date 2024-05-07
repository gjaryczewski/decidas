using Decidas.Areas.Structure.Models;
using Decidas.Areas.Structure.Policies;
using Decidas.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Decidas.Areas.Structure;

public record AssignKeeperRequest(Guid GroupId, Guid KeeperId, DateTime AssignDate);

public class AssignKeeperCommand(
    ILogger<AssignKeeperCommand> _logger,
    ApplicationDb _db,
    IOptions<FeatureFlags> _featureFlags,
    KeepingPolicy _keepingPolicy)
{
    public async Task ExecuteAsync(AssignKeeperRequest request, CancellationToken cancel)
    {
        _logger.LogInformation("Assigning keeper {keeperId} to group {groupId}", request.KeeperId, request.GroupId);

        var groupId = new GroupId(request.GroupId);
        var group = await _db.Groups.FirstOrDefaultAsync(g => g.Id == groupId, cancel)
            ?? throw new UnknownGroupForAssigningKeeper(request.GroupId);

        var keeperId = new KeeperId(request.KeeperId);
        group.AssignKeeper(keeperId, DateOnly.FromDateTime(request.AssignDate));

        if (_featureFlags.Value.EnableKeepingPolicy)
        {
            await _keepingPolicy.EvaluateAsync(keeperId, cancel);
        }

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
