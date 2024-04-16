using Cloudea.Domain.Common.Primitives;
using Cloudea.Domain.Identity.Enums;

namespace Cloudea.Domain.Identity.Entities
{
    public class Report : Entity, IAuditableEntity
    {
        public Report(
            Guid id,
            Guid userId,
            Guid targetUserId,
            Guid targetItemId,
            ReportItemType targetItemType,
            ReportStatus reportStatus,
            string description) : base(id)
        {
            UserId = userId;
            TargetUserId = targetUserId;
            TargetItemId = targetItemId;
            TargetItemType = targetItemType;
            Description = description;
            Status = reportStatus;
        }

        public Guid UserId { get; set; }
        public Guid TargetUserId { get; set; }
        public Guid TargetItemId { get; set; }
        public ReportItemType TargetItemType { get; set; }
        public ReportStatus Status { get; set; }
        public string Description { get; set; }

        public DateTimeOffset CreatedOnUtc { get; }

        public DateTimeOffset? ModifiedOnUtc { get; }

        public static Report? Create(
            Guid userId,
            Guid targetUserId,
            Guid targetItemId,
            ReportItemType targetItemType,
            string description,
            ReportStatus status = ReportStatus.Pending)
        {
            if (userId == Guid.Empty ||
                targetUserId == Guid.Empty ||
                targetItemId == Guid.Empty)
                return null;

            return new(
                Guid.NewGuid(),
                userId,
                targetUserId,
                targetItemId,
                targetItemType,
                status,
                description);
        }

    }
}
