namespace Cloudea.Application.Forum.Contracts.Request;

public sealed record UpdateSectionRequest(string Name, Guid? MasterId, string Statement);
