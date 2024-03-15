namespace Cloudea.Application.Forum.Contracts;

public sealed record UpdateSectionRequest(string Name, Guid? MasterId, string Statement);
