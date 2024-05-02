using Decidas.Core;
using Group = Decidas.Areas.Structure.Models.Group;
using Microsoft.AspNetCore.Mvc;
using Decidas.Areas.Structure.Models;

namespace Decidas.Areas.Structure.Features;

public record AssignKeeperRequest(Guid GroupId, Guid KeeperId, DateTime StartDate);

public class AssignKeeperCommand(ILogger<AssignKeeperCommand> _logger, ApplicationDb _db)
{
    public async Task<GroupId> ExecuteAsync(AssignKeeperRequest request, CancellationToken cancel)
    {
        _logger.LogInformation("Executing AssignKeeper command for group '{groupName}'", request.Name);

        var group = Group.Create(request.Name, DateOnly.FromDateTime(request.StartDate));

        await _db.Groups.AddAsync(group, cancel);
        await _db.SaveChangesAsync(cancel);

        return group.Id;
    }
}

[ApiController]
[Route("api/structure/groups/assign-keeper")]
public class AssignKeeperEndpoint(ILogger<AssignKeeperEndpoint> _logger, AssignKeeperCommand _command) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> HandleAsync([FromBody]AssignKeeperRequest request, CancellationToken cancel)
    {
        _logger.LogInformation("Handling AssignKeeper request for group '{groupName}'", request.Name);

        var response = await _command.ExecuteAsync(request, cancel);

        return CreatedAtAction(nameof(HandleAsync), new { GroupId = response.Value });
    }
}
