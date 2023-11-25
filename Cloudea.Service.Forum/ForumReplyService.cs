using Cloudea.Entity.Forum;
using Cloudea.Infrastructure.Database;

namespace Cloudea.Service.Forum
{
    public class ForumReplyService : BaseCurdService<Forum_Reply>
    {
        private readonly IFreeSql _database;

        public ForumReplyService(IFreeSql database) : base(database)
        {
            _database = database;
        }
    }
}
