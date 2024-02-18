using Cloudea.Entity.Forum;
using Cloudea.Infrastructure.Database;

namespace Cloudea.Service.Forum.Domain
{
    public class ForumReplyRepository : BaseRepository<Forum_Reply>
    {
        private readonly IFreeSql _database;

        public ForumReplyRepository(IFreeSql database) : base(database)
        {
            _database = database;
        }
    }
}
