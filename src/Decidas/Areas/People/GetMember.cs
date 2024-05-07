using Decidas.Areas.People.Models;
using Decidas.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Decidas.Areas.People;

public record GetMemberRequest(Guid Id);

public class GetMemberQuery(ILogger<GetMemberQuery> _logger, ApplicationDb _db)
{
    public async Task<MemberType?> ExecuteAsync(GetMemberRequest request, CancellationToken cancel)
    {
        _logger.LogInformation("Executing GetMember query for member ID {memberId}", request.Id);

        var memberId = new MemberId(request.Id);
        var member = await _db.Members.AsNoTracking()
            .FirstOrDefaultAsync(member => member.Id == memberId, cancel);

        return member is not null ? MemberType.FromMember(member) : null;
    }
}

[ApiController]
[Route("api/people/members")]
public class GetMemberEndpoint(ILogger<GetMemberEndpoint> _logger, GetMemberQuery _query) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MemberType>> HandleAsync(Guid id, CancellationToken cancel)
    {
        _logger.LogInformation("Handling GetMember request for member ID {id}", id);

        var request = new GetMemberRequest(id);

        var response = await _query.ExecuteAsync(request, cancel);

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }
}
