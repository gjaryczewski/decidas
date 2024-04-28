using Decidas.Core;
using Decidas.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Decidas.Areas.Groups.Features;

public record struct GetGroupListRequest(int Page, int PerPage);

public record struct GroupListItem(Guid Id, string Name, DateTime StartDate);

public class GroupList(int page, int perPage)
{
    public List<GroupListItem> Items { get; } = [];

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

public class GetGroupListQuery(ILogger<GetGroupListQuery> _logger, ApplicationDb _db)
{
    public async Task<GroupList?> ExecuteAsync(GetGroupListRequest request, CancellationToken cancel)
    {
        _logger.LogInformation("Executing GetGroupList query for page {page}", request.Page);

        var response = new GroupList(request.Page, request.PerPage);

        var groups = await _db.Groups.AsNoTracking()
            .Skip((response.Page - 1) * response.PerPage)
            .Take(response.PerPage)
            .ToListAsync(cancel);

        groups.ForEach(g => response.Items.Add(
            new GroupListItem(g.Id.Value, g.Name, g.StartDate.Value.ToDateTime())));

        return response;
    }
}

[ApiController]
[Route("api/groups")]
public class GetGroupListEndpoint(ILogger<GetGroupListEndpoint> _logger, GetGroupListQuery _query) : ControllerBase
{
    [HttpGet("{page:int=1}/{perPage:int=30}")]
    public async Task<ActionResult<GroupList>> HandleAsync(int page, int perPage, CancellationToken cancel)
    {
        _logger.LogInformation("Handling GetGroupList request for page {page}", page);

        var request = new GetGroupListRequest(page, perPage);

        var response = await _query.ExecuteAsync(request, cancel);

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }
}
