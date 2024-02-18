using Cloudea.Entity.Forum;
using Cloudea.Infrastructure.Models;
using Cloudea.Service.Forum.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Cloudea.Service.Forum.Domain
{
    /// <summary>
    /// Forum模块 的功能集合类
    /// </summary>
    public class ForumApplicationService
    {
        private readonly ForumDomainService _domainService;

        private readonly IForumTopicRepository _forumTopicRepository;
        private readonly IForumSectionRepository _forumSectionRepository;



        public ForumApplicationService(
            ForumDomainService domainService,
            IForumTopicRepository forumTopicRepository,
            IForumSectionRepository forumSectionRepository)
        {
            _domainService = domainService;
            _forumTopicRepository = forumTopicRepository;
            _forumSectionRepository = forumSectionRepository;
        }

        /// <summary>
        /// 发布主题帖
        /// </summary>
        public async Task<Result<Forum_Topic>> PostTopicAsync(
            PostTopicRequest request)
        {
            var createNewTopicRes = await _domainService.CreateTopic(request);
            if (createNewTopicRes.Status is false) {
                return createNewTopicRes;
            }

            // 插入实体
            var createRes = await _forumTopicRepository.SaveTopic(createNewTopicRes.Data);
            // 主题的帖子计数增加
            var increaseRes = await _forumSectionRepository.IncreaseTopicCount(request.sectionId);

            return Result.Success(createRes.Data);
        }

        /// <summary>
        /// 查看帖子列表
        /// </summary>
        public void ListTopic()
        {
            throw new NotImplementedException();
        }
    }
}
