namespace Cloudea.Application.Forum.Contracts;

public sealed record CreatePostRequest(Guid SectionId, string Title, string Content);
