using Decidas.Core;

namespace Decidas.Areas.Groups.Features;

public record struct CreateGroupRequest(string Name, DateTime StartDate);

public record struct CreateGroupResponse(Guid Id);

public sealed class CreateGroupCommand(ILogger<CreateGroupCommand> _logger, DomainEventCollector _domainEvents)
{
    public Task<CreateGroupResponse> ProcessAsync(CreateGroupRequest request, CancellationToken cancel)
    {
        _logger.LogInformation("Processing CreateGroup command for {groupName}", request.Name);

        ValidateRequest(request);

        CreateGroupResponse result = new(Guid.Empty);

        _domainEvents.Collect(new GroupCreatedEvent(result.Id));

        return Task.FromResult(result);
    }

    private static void ValidateRequest(CreateGroupRequest request)
    {
        var startDate = DateOnly.FromDateTime(request.StartDate);
        if (startDate < GroupsConstraints.OldestStartDate)
        {
            throw new TooOldStartDateError(startDate);
        }
    }
}

internal sealed class TooOldStartDateError : DomainError
{
    public TooOldStartDateError(DateOnly startDate)
    {
        Details = $"Start date {startDate} is earlier than oldest possible {GroupsConstraints.OldestStartDate}.";
    }
}

internal sealed class GroupCreatedEvent(Guid id) : DomainEvent(id) {}
