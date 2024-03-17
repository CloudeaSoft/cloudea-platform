using Cloudea.Domain.Common.Primitives;

namespace Cloudea.Domain.Forum.DomainEvents;

public sealed record PostCreatedDomainEvent(Guid Id, Guid PostId, Guid SectionId) : DomainEvent(Id);
