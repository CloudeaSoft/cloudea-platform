using Cloudea.Entity.Forum;
using Cloudea.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Forum.Domain
{
    public interface IForumSectionRepository
    {
        Task<Result<List<Forum_Section>>> List();

        Task<Result<long>> SaveSection(Forum_Section newSection);
    }
}
