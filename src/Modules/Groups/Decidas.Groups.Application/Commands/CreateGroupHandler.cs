using Decidas.Abstractions.Commands;
using Decidas.Abstractions.Events;
using Decidas.Groups.Application.Repositories;
using Decidas.Groups.Contracts.Commands;
using Decidas.Groups.Domain.Entities;
using Decidas.Groups.Domain.Events;

namespace Decidas.Groups.Application.Commands;

public class CreateGroupHandler(
    IGroupRepository repository,
    IEventDispatcher dispatcher) : ICommandHandler<CreateGroupCommand, CreateGroupResult>
{
    private readonly IGroupRepository _repository = repository;
    private readonly IEventDispatcher _dispatcher = dispatcher;

    public async Task<CreateGroupResult> HandleAsync(CreateGroupCommand command, CancellationToken cancellation)
    {
        var group = new Group(command.Name, command.StartDate);

        await _repository.AddAsync(group, cancellation);

        await _dispatcher.PublishAsync<GroupCreatedEvent>(new GroupCreatedEvent(group.Id, group.Name), cancellation);

        return CreateGroupResult.From(group);
    }
}