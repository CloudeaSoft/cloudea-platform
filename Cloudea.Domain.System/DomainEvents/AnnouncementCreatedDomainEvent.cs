using Cloudea.Domain.Common.Primitives;

namespace Cloudea.Domain.System.DomainEvents;

public sealed record AnnouncementCreatedDomainEvent(Guid Id, Guid AnnounceId) : DomainEvent(Id);
