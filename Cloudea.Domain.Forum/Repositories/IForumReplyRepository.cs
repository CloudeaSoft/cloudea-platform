using Cloudea.Infrastructure.Shared;
using Cloudea.Service.Forum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Forum.Domain.Repositories
{
    public interface IForumReplyRepository
    {
        Task<ForumReply?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<PageResponse<ForumReply>> GetByTopicIdWithPageRequestAsync(Guid topicId, PageRequest request, CancellationToken cancellationToken = default);

        void Add(ForumReply reply);
    }
}
