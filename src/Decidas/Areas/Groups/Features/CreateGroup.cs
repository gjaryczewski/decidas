using Decidas.Core;
using Group = Decidas.Areas.Groups.Models.Group;
using Microsoft.AspNetCore.Mvc;

namespace Decidas.Areas.Groups.Features;

public record struct CreateGroupRequest(string Name, DateTime StartDate);

public record struct CreatedGroupId(Guid Id);

public class CreateGroupCommand(ILogger<CreateGroupCommand> _logger, ApplicationDb _db)
{
    public async Task<CreatedGroupId> ExecuteAsync(CreateGroupRequest request, CancellationToken cancel)
    {
        _logger.LogInformation("Executing CreateGroup command for group '{groupName}'", request.Name);

        var group = Group.Create(request.Name, DateOnly.FromDateTime(request.StartDate));

        await _db.Groups.AddAsync(group, cancel);
        await _db.SaveChangesAsync(cancel);

        return new CreatedGroupId(group.Id.Value);
    }
}

[ApiController]
[Route("api/groups")]
public class CreateGroupEndpoint(ILogger<CreateGroupEndpoint> _logger, CreateGroupCommand _command) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CreatedGroupId>> HandleAsync([FromBody]CreateGroupRequest request, CancellationToken cancel)
    {
        _logger.LogInformation("Handling CreateGroup request for group '{groupName}'", request.Name);

        var response = await _command.ExecuteAsync(request, cancel);

        return CreatedAtAction(nameof(HandleAsync), response.Id);
    }
}
