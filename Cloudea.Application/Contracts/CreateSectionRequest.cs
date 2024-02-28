namespace Cloudea.Service.Forum.Domain.Models;

public sealed record CreateSectionRequest(string SectionName, Guid MasterId, string Statement);

