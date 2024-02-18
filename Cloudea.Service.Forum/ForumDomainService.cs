using Cloudea.Entity.Forum;
using Cloudea.Infrastructure.Models;
using Cloudea.Service.Forum.Domain.Abstractions;
using Cloudea.Service.Forum.Domain.Models;
using FluentValidation;
using Org.BouncyCastle.Asn1.X509.Qualified;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Forum.Domain
{
    public class ForumDomainService(IValidator<PostTopicRequest> postTopicRequestValidator)
    {
        private readonly IValidator<PostTopicRequest> _postTopicRequestValidator = postTopicRequestValidator;

        /// <summary>
        /// 创建Forum_Topic实体
        /// </summary>
        public async Task<Result<Forum_Topic>> CreateTopic(PostTopicRequest request)
        {
            // request合法性检查
            var validateRes = _postTopicRequestValidator.Validate(request);
            // request合法性检查未通过
            if (validateRes.IsValid is false) {
                return Result.Fail($"创建帖子失败。原因：{validateRes.Errors}");
            }

            // 创建Forum_Topic实体
            var newTopic = Forum_Topic.Create(
                request.userId,
                request.sectionId,
                request.title,
                request.content);

            return Result.Success(newTopic);
        }
    }
}
