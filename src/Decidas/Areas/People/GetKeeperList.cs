using Decidas.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Decidas.Areas.People;

public record KeeperListRequest(int Page, int PerPage);

public class KeeperList(int page, int perPage)
{
    public List<KeeperDetails> Items { get; } = [];

    public int Count => Items.Count;

    public int Page { get; } = page switch
    {
        <= 0 => 1,
        _ => page,
    };

    public int PerPage { get; } = perPage switch
    {
        <= 0 => 30,
        > 100 => 100,
        _ => perPage,
    };
}

public class GetKeeperListQuery(ILogger<GetKeeperListQuery> _logger, ApplicationDb _db)
{
    public async Task<KeeperList> ExecuteAsync(KeeperListRequest request, CancellationToken cancel)
    {
        _logger.LogInformation("Executing GetKeeperList query for page {page}", request.Page);

        var response = new KeeperList(request.Page, request.PerPage);

        var keepers = await _db.Keepers.AsNoTracking()
            .Include(keeper => keeper.Member)
            .OrderBy(keeper => keeper.Member.Name)
            .Skip((response.Page - 1) * response.PerPage)
            .Take(response.PerPage)
            .ToListAsync(cancel);

        keepers.ForEach(keeper => response.Items.Add(KeeperDetails.FromKeeper(keeper)));

        return response;
    }
}

[ApiController]
[Route("api/people/keepers")]
public class GetKeeperListEndpoint(ILogger<GetKeeperListEndpoint> _logger, GetKeeperListQuery _query) : ControllerBase
{
    [HttpGet("{page:int=1}/{perPage:int=30}")]
    public async Task<ActionResult<KeeperList>> HandleAsync(int page, int perPage, CancellationToken cancel)
    {
        _logger.LogInformation("Handling GetKeeperList request for page {page}", page);

        var request = new KeeperListRequest(page, perPage);

        var response = await _query.ExecuteAsync(request, cancel);

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }
}