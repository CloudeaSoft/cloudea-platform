namespace Cloudea.Application.Forum.Contracts.Request;

public sealed record CreateSectionRequest(string SectionName, Guid MasterId, string Statement);

