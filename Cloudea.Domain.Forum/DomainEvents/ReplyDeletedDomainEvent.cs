using Cloudea.Domain.Common.Primitives;

namespace Cloudea.Domain.Forum.DomainEvents;

public sealed record ReplyDeletedDomainEvent(Guid Id, Guid ReplyId, Guid PostId) : DomainEvent(Id);