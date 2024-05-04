using Decidas.Areas.People.Contracts;
using Decidas.Areas.People.Mappers;
using Decidas.Areas.People.Models;
using Decidas.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Decidas.Areas.People.Features;

public record GetKeeperRequest(Guid Id);

public class GetKeeperQuery(ILogger<GetKeeperQuery> _logger, ApplicationDb _db)
{
    public async Task<KeeperType?> ExecuteAsync(GetKeeperRequest request, CancellationToken cancel)
    {
        _logger.LogInformation("Executing GetKeeper query for keeper ID {id}", request.Id);

        var keeperId = new KeeperId(request.Id);

        var keeper = await _db.Keepers.AsNoTracking()
            .Include(keeper => keeper.Member)
            .FirstOrDefaultAsync(keeper => keeper.Id == keeperId, cancel);

        return keeper?.ToKeeperType();
    }
}

[ApiController]
[Route("api/people/keepers")]
public class GetKeeperEndpoint(ILogger<GetKeeperEndpoint> _logger, GetKeeperQuery _query) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<KeeperType>> HandleAsync(Guid id, CancellationToken cancel)
    {
        _logger.LogInformation("Handling GetKeeper request for keeper ID {id}", id);

        var request = new GetKeeperRequest(id);

        var response = await _query.ExecuteAsync(request, cancel);

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }
}
