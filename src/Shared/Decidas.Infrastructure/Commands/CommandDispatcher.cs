using Decidas.Abstractions.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Decidas.Infrastructure.Commands;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _services;

    public CommandDispatcher(IServiceProvider services) => _services = services;

    public Task<TResult> SendAsync<TCommand, TResult>(TCommand command, CancellationToken cancellation)
    {
        var handler = _services.GetRequiredService<ICommandHandler<TCommand, TResult>>();

        return handler.HandleAsync(command, cancellation);
    }
}