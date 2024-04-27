using Decidas.Core;
using Group = Decidas.Areas.Groups.Models.Group;

namespace Decidas.Areas.Groups.Features;

public record struct CreateGroupRequest(string Name, DateTime StartDate);

public record struct CreateGroupResponse(Guid Id);

public class CreateGroupCommand(ILogger<CreateGroupCommand> _logger, ApplicationDb _db)
{
    public async Task<CreateGroupResponse> ProcessAsync(CreateGroupRequest request, CancellationToken cancel)
    {
        _logger.LogInformation("Processing CreateGroup command for {groupName}", request.Name);

        var group = Group.Create(request.Name, DateOnly.FromDateTime(request.StartDate));

        await _db.Groups.AddAsync(group);
        await _db.SaveChangesAsync();

        return new CreateGroupResponse(group.Id.Value);
    }
}
