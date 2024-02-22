using Cloudea.Entity.Forum;
using Cloudea.Infrastructure.Database;
using Cloudea.Infrastructure.Models;
using Cloudea.Service.Forum.Domain.Repositories;
using System.Xml;

namespace Cloudea.Service.Forum.Domain
{
    public class ForumTopicRepository : BaseRepository<Forum_Topic>, IForumTopicRepository {
        public ForumTopicRepository(IFreeSql freeSql) : base(freeSql) {
            _database = freeSql;
        }

        public async Task<Result<Forum_Topic>> Read(Guid id) {
            return await base.Read(id);
        }

        public async Task<Result<PageResponse<Forum_Topic>>> List(PageRequest request) {
            return await base.GetBaseList(request);
        }

        public async Task<Result<long>> SaveTopic(Forum_Topic newTopic) {
            return await Create(newTopic);
        }
    }
}
