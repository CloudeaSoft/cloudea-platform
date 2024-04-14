namespace Cloudea.Application.Forum.Contracts.Request;

public sealed record CreatePostRequest(Guid SectionId, string Title, string Content);
