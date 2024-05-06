using Decidas.Areas.People.Models;
using Decidas.Core;
using Decidas.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Decidas.Areas.People.Features;

public record KeeperDetailsRequest(Guid Id);

public record KeeperDetails(Guid Id, Guid MemberId, string Name, string Email, DateTime DesignateDate)
{
    public static KeeperDetails FromKeeper(Keeper keeper) => new(
        keeper.Id.Value,
        keeper.MemberId.Value,
        keeper.Member.Name,
        keeper.Member.Email.Value,
        keeper.DesignateDate.ToDateTime()
    );
}

public class GetKeeperDetailsCommand(ILogger<GetKeeperDetailsCommand> _logger, ApplicationDb _db)
{
    public async Task<KeeperDetails?> ExecuteAsync(KeeperDetailsRequest request, CancellationToken cancel)
    {
        _logger.LogInformation("Getting details of keeper ID {id}", request.Id);

        var keeperId = new KeeperId(request.Id);
        var keeper = await _db.Keepers.AsNoTracking()
            .Include(keeper => keeper.Member)
            .FirstOrDefaultAsync(keeper => keeper.Id == keeperId, cancel);

        return keeper is null ? null : KeeperDetails.FromKeeper(keeper);
    }
}

[ApiController]
[Route("api/people/keepers")]
public class GetKeeperDetailsEndpoint(ILogger<GetKeeperDetailsEndpoint> _logger, GetKeeperDetailsCommand _query) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<KeeperDetails>> HandleAsync(Guid id, CancellationToken cancel)
    {
        _logger.LogInformation("API request for getting details of keeper ID {id}", id);

        var request = new KeeperDetailsRequest(id);

        var response = await _query.ExecuteAsync(request, cancel);

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }
}
