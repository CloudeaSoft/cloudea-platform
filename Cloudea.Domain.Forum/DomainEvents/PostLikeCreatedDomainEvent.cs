using Cloudea.Domain.Common.Primitives;

namespace Cloudea.Domain.Forum.DomainEvents;

public sealed record PostLikeCreatedDomainEvent(Guid Id, Guid LikeId, Guid PostId) : DomainEvent(Id);

