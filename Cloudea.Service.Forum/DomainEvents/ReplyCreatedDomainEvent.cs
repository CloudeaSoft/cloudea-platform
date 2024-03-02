using Cloudea.Service.Forum.Domain.DomainEvents;

namespace Cloudea.Service.Forum.DomainEvents;

public sealed record ReplyCreatedDomainEvent(Guid Id, Guid ReplyId, Guid PostId) : DomainEvent(Id);
