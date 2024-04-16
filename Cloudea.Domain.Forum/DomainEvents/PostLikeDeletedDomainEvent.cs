using Cloudea.Domain.Common.Primitives;

namespace Cloudea.Domain.Forum.DomainEvents;

public sealed record PostLikeDeletedDomainEvent(Guid Id, Guid LikeId, Guid PostId) : DomainEvent(Id);
