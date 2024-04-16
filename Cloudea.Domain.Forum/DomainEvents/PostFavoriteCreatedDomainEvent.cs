using Cloudea.Domain.Common.Primitives;

namespace Cloudea.Domain.Forum.DomainEvents;

public sealed record PostFavoriteCreatedDomainEvent(Guid Id, Guid FavoriteId, Guid PostId) : DomainEvent(Id);