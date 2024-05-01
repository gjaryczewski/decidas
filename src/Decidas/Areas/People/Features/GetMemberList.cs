using Decidas.Areas.People.Models;
using Decidas.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Decidas.Areas.People.Features;

public record struct GetMemberListRequest(int Page, int PerPage);

public class MemberList(int page, int perPage)
{
    public List<MemberType> Items { get; } = [];

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

public class GetMemberListQuery(ILogger<GetMemberListQuery> _logger, ApplicationDb _db)
{
    public async Task<MemberList?> ExecuteAsync(GetMemberListRequest request, CancellationToken cancel)
    {
        _logger.LogInformation("Executing GetMemberList query for page {page}", request.Page);

        var response = new MemberList(request.Page, request.PerPage);

        var members = await _db.Members.AsNoTracking()
            .OrderBy(group => group.Name)
            .Skip((response.Page - 1) * response.PerPage)
            .Take(response.PerPage)
            .ToListAsync(cancel);

        members.ForEach(member => response.Items.Add(MemberType.FromMember(member)));

        return response;
    }
}

[ApiController]
[Route("api/people/members")]
public class GetMemberListEndpoint(ILogger<GetMemberListEndpoint> _logger, GetMemberListQuery _query) : ControllerBase
{
    [HttpGet("{page:int=1}/{perPage:int=30}")]
    public async Task<ActionResult<MemberList>> HandleAsync(int page, int perPage, CancellationToken cancel)
    {
        _logger.LogInformation("Handling GetMemberList request for page {page}", page);

        var request = new GetMemberListRequest(page, perPage);

        var response = await _query.ExecuteAsync(request, cancel);

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }
}
