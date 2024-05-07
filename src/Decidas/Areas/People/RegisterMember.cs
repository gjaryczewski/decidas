using Decidas.Core;
using Decidas.Areas.People.Models;
using Microsoft.AspNetCore.Mvc;

namespace Decidas.Areas.People;

public record RegisterMemberRequest(string Name, string Email, string Password);

public class RegisterMemberCommand(ILogger<RegisterMemberCommand> _logger, ApplicationDb _db)
{
    public async Task<MemberId> ExecuteAsync(RegisterMemberRequest request, CancellationToken cancel)
    {
        _logger.LogInformation("Executing RegisterMember command for email {email}", request.Email);

        var member = Member.Register(request.Name, request.Email, request.Password);

        await _db.Members.AddAsync(member, cancel);
        await _db.SaveChangesAsync(cancel);

        return member.Id;
    }
}

[ApiController]
[Route("api/people/members")]
public class RegisterMemberEndpoint(ILogger<RegisterMemberEndpoint> _logger, RegisterMemberCommand _command) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> HandleAsync([FromBody]RegisterMemberRequest request, CancellationToken cancel)
    {
        _logger.LogInformation("Handling RegisterMember request for email {email}", request.Email);

        var response = await _command.ExecuteAsync(request, cancel);

        return CreatedAtAction(nameof(HandleAsync), new { MemberId = response.Value });
    }
}
