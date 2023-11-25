using Cloudea.Entity.Forum;
using Cloudea.Infrastructure.Database;
using Cloudea.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Forum
{
    public class ForumTopicService : BaseCurdService<Forum_Topic>
    {
        private readonly IFreeSql _database;

        public ForumTopicService(IFreeSql freeSql):base(freeSql)
        {
            _database = freeSql;
        }
    }
}
