using Decidas.Core;
using Decidas.Areas.People.Models;
using Microsoft.AspNetCore.Mvc;

namespace Decidas.Areas.People.Features;

public record DesignateKeeperRequest(Guid MemberId, DateTime DesignateDate);

public class DesignateKeeperCommand(ILogger<DesignateKeeperCommand> _logger, ApplicationDb _db)
{
    public async Task<KeeperId> ExecuteAsync(DesignateKeeperRequest request, CancellationToken cancel)
    {
        _logger.LogInformation("Executing DesignateKeeper command for member {memberId}", request.MemberId);

        var keeper = Keeper.Designate(request.MemberId, DateOnly.FromDateTime(request.DesignateDate));

        await _db.Keepers.AddAsync(keeper, cancel);
        await _db.SaveChangesAsync(cancel);

        return keeper.Id;
    }
}

[ApiController]
[Route("api/people/keepers")]
public class DesignateKeeperEndpoint(ILogger<DesignateKeeperEndpoint> _logger, DesignateKeeperCommand _command) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> HandleAsync([FromBody]DesignateKeeperRequest request, CancellationToken cancel)
    {
        _logger.LogInformation("Handling DesignateKeeper request for member {memberId}", request.MemberId);

        var response = await _command.ExecuteAsync(request, cancel);

        return CreatedAtAction(nameof(HandleAsync), new { KeeperId = response.Value });
    }
}
