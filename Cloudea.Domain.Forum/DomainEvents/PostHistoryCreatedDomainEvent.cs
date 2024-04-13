using Cloudea.Domain.Common.Primitives;

namespace Cloudea.Domain.Forum.DomainEvents;

public sealed record PostHistoryCreatedDomainEvent(Guid Id, Guid HistoryId, Guid PostId) : DomainEvent(Id);
