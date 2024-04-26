using Decidas.Core;

namespace Decidas.Areas.Groups.Features;

public record struct CreateGroupRequest(string Name, DateOnly StartDate);

public record struct CreateGroupResponse(Guid Id);

public sealed class CreateGroupCommand(ILogger<CreateGroupCommand> _logger, DomainEventCollector _domainEvents)
{
    public Task<CreateGroupResponse> ProcessAsync(CreateGroupRequest request)
    {
        _logger.LogInformation("Processing CreateGroup command for {groupName}", request.Name);

        ValidateRequest(request);

        CreateGroupResponse result = new(Guid.Empty);

        _domainEvents.Collect(new GroupCreatedEvent(result.Id));

        return Task.FromResult(result);
    }

    private void ValidateRequest(CreateGroupRequest request)
    {
        if (request.StartDate < GroupsConstraints.OldestStartDate)
        {
            throw new TooOldStartDateError(request.StartDate);
        }
    }
}

internal sealed class TooOldStartDateError : DomainError
{
    public TooOldStartDateError(DateOnly startDate)
    {
        Location = typeof(CreateGroupCommand).FullName!;
        Code = nameof(TooOldStartDateError);
        Details = $"Start date {startDate} is earlier than oldest possible {GroupsConstraints.OldestStartDate}.";
    }
}

internal sealed class GroupCreatedEvent(Guid id) : DomainEvent(id) {}
