using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Decidas.Web.Pages.Groups;

public class CreateModel : PageModel
{
    private readonly ICommandDispatcher _dispatcher;
    private readonly ILogger<CreateModel> _logger;

    public CreateModel(ICommandDispatcher dispatcher, ILogger<CreateModel> logger)
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
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var command = new CreateGroupCommand(
            RequestModel.Name,
            DateOnly.FromDateTime(RequestModel.StartDate.Date));

        var result = await _dispatcher.Send(command, cancel);

        if (result.IsFailure)
        {
            return Page();
        }
        else if (returnUrl is null) ??= Url.Content("~/");
)

        return LocalRedirect(returnUrl);
    }
}

public class CreateGroupRequest
{
    [Required]
    public string Name { get; set; }

    public DateTime StartDate { get; set; }
}

public record DomainEvent(Guid Id, string Type, DateTime Timestamp, string Payload);

public abstract class Entity
{
    public Guid Id { get; private init; }

    protected readonly List<DomainEvent> _domainEvents = [];

    protected Entity(Guid id)
    {
        Id = id;
    }

    public List<DomainEvent> PopDomainEvents()
    {
        var copy = _domainEvents.ToList();
        _domainEvents.Clear();

        return copy;
    }

    protected Entity() { }
}

public class Group : Entity
{
    public string Name { get; private set; }

    public DateOnly StartDate { get; private set; }

    public Group(string name, DateOnly startDate)
    {
        Name = name;
        StartDate = startDate;
    }
}

public class AppDbContext(DbContextOptions _options) : DbContext(_options)
{
    public DbSet<Group> Groups { get; set; } = null!;

    public DbSet<DomainEvent> DomainEvents { get; set; } = null!;

    public async override Task<int> SaveChangesAsync(CancellationToken cancel = default)
    {
        PublishDomainEvents();

        return await base.SaveChangesAsync(cancel);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    private void PublishDomainEvents()
    {
        var domainEvents = ChangeTracker.Entries<Entity>()
           .SelectMany(entry => entry.Entity.PopDomainEvents())
           .ToList();

        DomainEvents.AddRange(domainEvents);
    }
}

public interface IGroupRepository
{
    Task Add(Group group, CancellationToken cancel);

    Task SaveChanges();
}

public class GroupRepository(AppDbContext _dbcontext) : IGroupRepository
{
    public async Task Add(Group group, CancellationToken cancel)
    {
        await _dbcontext.AddAsync(group, cancel);
    }

    public async Task SaveChanges()
    {
        await _dbcontext.SaveChangesAsync();
    }
}

public interface ICommand;

public record CreateGroupCommand(string Name, DateOnly StartDate) : ICommand;

public record Error(string Code, string Details)
{
    public DateTime Timestamp => DateTime.Now;

    public static Error FromException(Exception ex)
    {
        return new Error(ex.GetType().Name, ex.StackTrace);
    }
};

public class Result
{
    public string Message { get; private set; }

    public bool IsSuccess { get; private set; }

    public bool IsFailure => !IsSuccess;

    public List<Error> Errors { get; private set; } = [];

    private Result(string message, bool isSuccess)
    {
        Message = message;
        IsSuccess = isSuccess;
    }

    public static Result WithSuccessMessage(string message = "OK") => new(message, true);
 
    public static Result WithFailureMessage(string message = "An error occured.") => new(message, true);

    public void AppendError(Error error)
    {
        Errors.Add(error);
    }
}

public static class ResultExtensions
{
    public static Result WithError(this Result result, Error error)
    {
        result.AppendError(error);

        return result;
    }
}

public interface ICommandDispatcher
{
    Task<Result> Send<T>(T command, CancellationToken cancel);
}

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _services;

    public CommandDispatcher(IServiceProvider services) => _services = services;

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

public class CreateGroupCommandHandler : ICommandHandler<CreateGroupCommand>
{
    private readonly IGroupRepository _repository;
    private readonly ILogger<CreateGroupCommandHandler> _logger;

    public CreateGroupCommandHandler(
        IGroupRepository repository,
        ILogger<CreateGroupCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result> Handle(CreateGroupCommand command, CancellationToken cancel)
    {
        _logger.LogDebug($"Handling {nameof(CreateGroupCommand)}");

        var group = new Group(command.Name, command.StartDate);

        await _repository.Add(group, cancel);

        try
        {
            await _repository.SaveChanges();
            return Result.WithSuccessMessage();
        }
        catch(Exception ex)
        {
            return Result.WithFailureMessage().WithError(Error.FromException(ex));
        }
    }
}
