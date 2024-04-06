using Decidas.Abstractions;

namespace Decidas.Groups.Contracts.Responses;

public record GetGroupResponse(Guid GroupId, string GroupName) : IResponse;