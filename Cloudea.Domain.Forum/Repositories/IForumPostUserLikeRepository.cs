using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Forum.Entities;

namespace Cloudea.Domain.Forum.Repositories;

public interface IForumPostUserLikeRepository
{
    void Add(ForumPostUserLike like);

    void Delete(ForumPostUserLike like);

    Task<ForumPostUserLike?> GetByUserIdPostIdAsync(Guid userId, Guid postId,CancellationToken cancellationToken = default);

    Task<List<ForumPostUserLike>> ListByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<PageResponse<Guid>> ListPostIdWithPageRequestByUserIdAsync(Guid userId, PageRequest pageRequest, CancellationToken cancellationToken = default);
}
