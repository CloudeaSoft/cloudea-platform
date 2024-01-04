using Cloudea.Entity.Forum;
using Cloudea.Infrastructure.Database;
using Cloudea.Infrastructure.Models;
using System.Xml;

namespace Cloudea.Service.Forum.Domain
{
    public class ForumTopicRepository : BaseCurdService<Forum_Topic>, IForumTopicRepository
    {
        private readonly IFreeSql _database;

        public ForumTopicRepository(IFreeSql freeSql) : base(freeSql)
        {
            _database = freeSql;
        }

        public async Task<Result<Forum_Topic>> Get(Guid id)
        {
            return await Read(id);
        }

        public async Task<Result<List<Forum_Topic>>> List(Guid sectionId)
        {
            await _database.Select<Forum_Topic>().ToListAsync();

            return await Read();
        }

        public Task<Result<List<Forum_Topic>>> List()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<long>> SaveTopic(Forum_Topic newTopic)
        {
            return await Create(newTopic);
        }
    }
}
