using Cloudea.Entity.Forum;
using Cloudea.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Forum.Domain
{
    public interface IForumTopicRepository
    {
        Task<Result<long>> SaveTopic(Forum_Topic newTopic);

        Task<Result<Forum_Topic>> Get(Guid id);

        Task<Result<List<Forum_Topic>>> List();
    }
}
