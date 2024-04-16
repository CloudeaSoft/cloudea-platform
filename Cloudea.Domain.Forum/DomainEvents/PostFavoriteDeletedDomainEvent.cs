using Cloudea.Domain.Common.Primitives;

namespace Cloudea.Domain.Forum.DomainEvents;

public sealed record PostFavoriteDeletedDomainEvent(Guid Id, Guid FavoriteId, Guid PostId) : DomainEvent(Id);