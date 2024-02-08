using Cloudea.Entity.Forum;
using Cloudea.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Forum.Domain {
    /// <summary>
    /// Forum模块 的功能集合类
    /// </summary>
    public class ForumService {
        private readonly ForumDomainService _domainService;

        private readonly IForumTopicRepository _forumTopicRepository;
        private readonly IForumSectionRepository _forumSectionRepository;

        public ForumService(ForumDomainService domainService, IForumTopicRepository forumTopicRepository, IForumSectionRepository forumSectionRepository) {
            _domainService = domainService;
            _forumTopicRepository = forumTopicRepository;
            _forumSectionRepository = forumSectionRepository;
        }

        /// <summary>
        /// 发帖
        /// </summary>
        public async Task<Result<long>> PostTopicAsync(
            Guid userId,
            Guid sectionId,
            string title,
            string content) {
            ///Todo 数据校验
            throw new NotImplementedException();

            return await _domainService.PostTopicAsync(userId, sectionId, title, content);
        }

        /// <summary>
        /// 查看帖子列表
        /// </summary>
        public void ListTopic() {
            throw new NotImplementedException();
        }
    }
}
