using Decidas.Core;
using Group = Decidas.Areas.Groups.Models.Group;
using Microsoft.AspNetCore.Mvc;

namespace Decidas.Areas.Groups.Features;

public record struct CreateGroupRequest(string Name, DateTime StartDate);

public record struct CreateGroupResponse(Guid Id);

public class CreateGroupCommand(ILogger<CreateGroupCommand> _logger, ApplicationDb _db)
{
    public async Task<CreateGroupResponse> ProcessAsync(CreateGroupRequest request, CancellationToken cancel)
    {
        _logger.LogInformation("Processing CreateGroup command for {groupName}", request.Name);

        var group = Group.Create(request.Name, DateOnly.FromDateTime(request.StartDate));

        await _db.Groups.AddAsync(group, cancel);
        await _db.SaveChangesAsync(cancel);

        return new CreateGroupResponse(group.Id.Value);
    }
}

[ApiController]
[Route("api/groups")]
public class CreateGroupEndpoint(CreateGroupCommand _command) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CreateGroupResponse>> HandleAsync([FromBody]CreateGroupRequest request, CancellationToken cancel)
    {
        var response = await _command.ProcessAsync(request, cancel);

        return CreatedAtAction(nameof(HandleAsync), response.Id);
    }
}
