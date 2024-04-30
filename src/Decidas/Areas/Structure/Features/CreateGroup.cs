using Decidas.Core;
using Group = Decidas.Areas.Structure.Models.Group;
using Microsoft.AspNetCore.Mvc;
using Decidas.Areas.Structure.Models;

namespace Decidas.Areas.Structure.Features;

public record struct CreateGroupRequest(string Name, DateTime StartDate);

public class CreateGroupCommand(ILogger<CreateGroupCommand> _logger, ApplicationDb _db)
{
    public async Task<GroupId> ExecuteAsync(CreateGroupRequest request, CancellationToken cancel)
    {
        _logger.LogInformation("Executing CreateGroup command for group '{groupName}'", request.Name);

        var group = Group.Create(request.Name, DateOnly.FromDateTime(request.StartDate));

        await _db.Groups.AddAsync(group, cancel);
        await _db.SaveChangesAsync(cancel);

        return group.Id;
    }
}

[ApiController]
[Route("api/structure/groups")]
public class CreateGroupEndpoint(ILogger<CreateGroupEndpoint> _logger, CreateGroupCommand _command) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> HandleAsync([FromBody]CreateGroupRequest request, CancellationToken cancel)
    {
        _logger.LogInformation("Handling CreateGroup request for group '{groupName}'", request.Name);

        var response = await _command.ExecuteAsync(request, cancel);

        return CreatedAtAction(nameof(HandleAsync), new { GroupId = response.Value });
    }
}
