namespace Cloudea.Application.Forum.Contracts;

public sealed record CreateSectionRequest(string SectionName, Guid MasterId, string Statement);

