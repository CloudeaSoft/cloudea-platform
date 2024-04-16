using Cloudea.Domain.Common.Primitives;

namespace Cloudea.Domain.Forum.DomainEvents;

public sealed record PostDislikeDeletedDomainEvent(Guid Id, Guid DislikeId, Guid PostId) : DomainEvent(Id);