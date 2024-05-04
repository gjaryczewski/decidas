using Decidas.Areas.People.Models;

namespace Decidas.Areas.Structure.Models;

public record Assignment(GroupId GroupId, KeeperId KeeperId, DateOnly AssignDate);

#region  Types

public record KeeperId(Guid Value);

#endregion
