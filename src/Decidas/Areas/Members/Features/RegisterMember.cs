using Decidas.Core;
using Decidas.Areas.Members.Models;
using Microsoft.AspNetCore.Mvc;

namespace Decidas.Areas.Members.Features;

public record struct RegisterMemberRequest(string Name, string Login, string Email, string Password);

public class RegisterMemberCommand(ILogger<RegisterMemberCommand> _logger, ApplicationDb _db)
{
    public async Task<MemberId> ExecuteAsync(RegisterMemberRequest request, CancellationToken cancel)
    {
        _logger.LogInformation("Executing RegisterMember command for login `{login}", request.Login);

        var member = Member.Create(request.Name, request.Login, request.Email, request.Password);

        await _db.Members.AddAsync(member, cancel);
        await _db.SaveChangesAsync(cancel);

        return member.Id;
    }
}

[ApiController]
[Route("api/members")]
public class RegisterMemberEndpoint(ILogger<RegisterMemberEndpoint> _logger, RegisterMemberCommand _command) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> HandleAsync([FromBody]RegisterMemberRequest request, CancellationToken cancel)
    {
        _logger.LogInformation("Handling RegisterMember request for login `{login}", request.Login);

        var response = await _command.ExecuteAsync(request, cancel);

        return CreatedAtAction(nameof(HandleAsync), new { MemberId = response.Value });
    }
}
