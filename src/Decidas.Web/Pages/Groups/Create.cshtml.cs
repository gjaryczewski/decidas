using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Decidas.Web.Pages.Groups;

public class CreateModel : PageModel
{
    private readonly IDispatcher _dispatcher;
    private readonly ILogger<CreateModel> _logger;

    public CreateModel(IDispatcher dispatcher, ILogger<CreateModel> logger)
    {
        _dispatcher = dispatcher;
        _logger = logger;
    }

    public void OnGet()
    {

    }

    [BindProperty]
    public CreateGroupRequest RequestModel { get; set; }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null, CancellationToken cancel = default)
    {
        returnUrl ??= Url.Content("~/");

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var command = new CreateGroupCommand(
            RequestModel.Name,
            DateOnly.FromDateTime(RequestModel.StartDate.Date));

        var result = await _dispatcher.Send(command, cancel);

        return LocalRedirect(returnUrl);
    }
}

public record CreateGroupRequest(string Name, DateTime StartDate);

public interface ICommand;

public record CreateGroupCommand(string Name , DateOnly StartDate) : ICommand;

public record Result(bool IsSuccess); 

public interface IDispatcher
{
    Task<Result> Send<T>(T command, CancellationToken cancel);
}

public class Dispatcher : IDispatcher
{
    private readonly IServiceProvider _services;

    public Dispatcher(IServiceProvider services) => _services = services;

    public Task<Result> Send<CreateGroupCommand>(CreateGroupCommand command, CancellationToken cancel)
    {
        var handler = _services.GetRequiredService<ICommandHandler<CreateGroupCommand>>();

        return handler.Handle(command, cancel);
    }
}

public interface ICommandHandler<T>
{
    Task<Result> Handle(T command, CancellationToken cancel);
}

public class CreateGroupHandler : ICommandHandler<CreateGroupCommand>
{
    private readonly ILogger<CreateGroupHandler> _logger;

    public CreateGroupHandler(ILogger<CreateGroupHandler> logger)
    {
        _logger = logger;
    }

    public Task<Result> Handle(CreateGroupCommand command, CancellationToken cancel)
    {
         _logger.LogInformation($"The group '{command.Name}' has been created.");

       return Task.FromResult(new Result(true));
    }
}