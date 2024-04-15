using Cloudea.Domain.Common.Primitives;

namespace Cloudea.Domain.Identity.DomainEvents;

public sealed record UserProfileAvatarUpdatedDomainEvent(Guid Id, Guid UserId, Uri? OldAvatarUri) : DomainEvent(Id);
