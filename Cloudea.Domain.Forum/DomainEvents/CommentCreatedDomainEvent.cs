using Cloudea.Domain.Common.Primitives;

namespace Cloudea.Domain.Forum.DomainEvents;

public sealed record CommentCreatedDomainEvent(Guid Id, Guid CommentId, Guid ReplyId) : DomainEvent(Id);
