using Cloudea.Domain.Common.Primitives;

namespace Cloudea.Domain.Forum.DomainEvents;

public sealed record ReplyCreatedDomainEvent(Guid Id, Guid ReplyId, Guid PostId) : DomainEvent(Id);
