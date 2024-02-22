using Cloudea.Entity.Forum;
using Cloudea.Infrastructure.Models;

namespace Cloudea.Service.Forum.Domain.Repositories
{
    public interface IForumTopicRepository
    {
        Task<Result<long>> SaveTopic(Forum_Topic newTopic);
        Task<Result<Forum_Topic>> Read(Guid id);
        Task<Result<PageResponse<Forum_Topic>>> List(PageRequest request);
    }
}
