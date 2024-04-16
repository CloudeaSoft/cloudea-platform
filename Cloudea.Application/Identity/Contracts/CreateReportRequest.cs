using Cloudea.Domain.Identity.Enums;

namespace Cloudea.Application.Identity.Contracts;

public sealed record CreateReportRequest(Guid TargetUserId, Guid TargetItemId, ReportItemType TargetItemType, string Description);

