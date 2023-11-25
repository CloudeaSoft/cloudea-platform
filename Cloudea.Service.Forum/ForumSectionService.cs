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
    public class ForumSectionService : BaseCurdService<Forum_Section>
    {
        private readonly IFreeSql _database;

        public ForumSectionService(IFreeSql database) : base(database)
        {
            _database = database;
        }

        public override async Task<Result<long>> Create(Forum_Section entity)
        {
            var res = await base.Exist(t => t.Id == entity.Id && t.Name != entity.Name);
            if (res is false) {
                return Result.Fail();
            }
            return await base.Create(entity);
        }
    }
}
