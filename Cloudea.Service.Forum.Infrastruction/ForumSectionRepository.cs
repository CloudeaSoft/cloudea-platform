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
    public class ForumSectionRepository(IFreeSql database) : BaseRepository<Forum_Section>(database), IForumSectionRepository
    {
        public async Task<Result<Forum_Section>> ReadSection(Guid sectionId)
        {
            return await Read(sectionId);
        }

        public async Task<Result<List<Forum_Section>>> ListSection()
        {
            return await Read();
        }

        public async Task<Result<long>> CreateSection(Forum_Section newSection)
        {
            return await Create(newSection);
        }

        public Task<Result> IncreaseTopicCount(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DecreaseTopicCount(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
