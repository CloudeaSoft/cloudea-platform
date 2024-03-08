using Cloudea.Infrastructure.Primitives;

namespace Cloudea.Service.Forum.Domain.DomainEvents;

public sealed record PostCreatedDomainEvent(Guid Id, Guid PostId, Guid SectionId) : DomainEvent(Id);
