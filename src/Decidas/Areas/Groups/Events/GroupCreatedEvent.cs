using Decidas.Core;

namespace Decidas.Areas.Groups.Models;

public class GroupCreatedEvent(Guid id) : DomainEvent(id) {}
