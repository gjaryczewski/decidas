using Decidas.Areas.People.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Decidas.Areas.People.Pages;

public class RegisterMemberModel(RegisterMemberCommand _command) : PageModel
{
    [BindProperty]
    public string Name { get; set; } = string.Empty;

    [BindProperty]
    public string Login { get; set; } = string.Empty;
    
    [BindProperty]
    public string Email { get; set; } = string.Empty;
    
    [BindProperty]
    public string Password { get; set; } = string.Empty;

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancel)
    {
        var request = new RegisterMemberRequest(Name, Login, Email, Password);

        var response = await _command.ExecuteAsync(request, cancel);

        return RedirectToPage("MemberDetails", new { id = response.Value } );
    }
}
