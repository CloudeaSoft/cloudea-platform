namespace Cloudea.Application.Contracts;

public sealed record UpdateSectionRequest(string Name, Guid? MasterId, string Statement);
