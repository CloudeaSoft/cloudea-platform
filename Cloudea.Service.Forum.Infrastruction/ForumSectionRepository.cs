using Cloudea.Entity.Forum;
using Cloudea.Infrastructure.Database;
using Cloudea.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Forum.Domain
{
    public class ForumSectionRepository : BaseCurdService<Forum_Section>, IForumSectionRepository
    {
        private readonly IFreeSql _database;

        public ForumSectionRepository(IFreeSql database) : base(database)
        {
            
        }

        public Task<Result> IncreaseTopicCount(Guid id) {
            throw new NotImplementedException();
        }

        public async Task<Result<List<Forum_Section>>> List()
        {
            return await Read();
        }

        public async Task<Result<long>> SaveSection(Forum_Section newSection)
        {
            return await Create(newSection);
        }
    }
}
