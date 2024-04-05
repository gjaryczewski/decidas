using Decidas.Abstractions;
namespace Decidas.Groups.Contracts;

public record CreateGroupCommand(Guid OwnerId, string Currency) : ICommand
{
    public Guid WalletId { get; init; } = Guid.NewGuid();
}