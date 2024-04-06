using Decidas.Abstractions;
using Decidas.Groups.Contracts.Responses;

namespace Decidas.Groups.Contracts.Queries;

public record GetGroupQuery(Guid GroupId) : IQuery<GetGroupResponse>;