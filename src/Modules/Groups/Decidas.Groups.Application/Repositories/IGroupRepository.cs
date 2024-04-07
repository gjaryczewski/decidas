using Decidas.Groups.Domain.Entities;

namespace Decidas.Groups.Application.Repositories;

public interface IGroupRepository
{
    Task AddAsync(Group group, CancellationToken cancellation);
}