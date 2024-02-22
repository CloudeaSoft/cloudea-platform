using Cloudea.Entity.Forum;
using Cloudea.Infrastructure.Models;
using Cloudea.Service.Forum.Domain.Models;
using FluentValidation;

namespace Cloudea.Service.Forum.Domain {
    public class ForumDomainService(IValidator<PostTopicRequest> postTopicRequestValidator, IValidator<PostSectionRequest> postSectionRequestValidator) {
        private readonly IValidator<PostTopicRequest> _postTopicRequestValidator = postTopicRequestValidator;
        private readonly IValidator<PostSectionRequest> _postSectionRequestValidator = postSectionRequestValidator;

        /// <summary>
        /// 修改Forum_Section实体列表
        /// </summary>
        /// <param name="sectionList"></param>
        /// <param name="updateRequestList"></param>
        /// <returns></returns>
        public Result<List<Forum_Section>> UpdateSectionList(
            List<Forum_Section> sectionList,
            List<UpdateSectionRequest> updateRequestList) {
            foreach (var section in sectionList) {
                for (int i = 0; i < updateRequestList.Count; i++) {
                    if (section.Id != updateRequestList[i].Id) {
                        continue;
                    }
                    section.UpdateSection(updateRequestList[i]);
                    break;
                }
            }
            return Result<List<Forum_Section>>.Success(sectionList);
        }

        /// <summary>
        /// 创建Forum_Section实体
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Result<Forum_Section> CreateSection(PostSectionRequest request) {
            var validateRes = _postSectionRequestValidator.Validate(request);
            if (validateRes.IsValid is false) {
                return new Error(validateRes.Errors.ToString()!);
            }
            var newSection = Forum_Section.Create(
                request.SectionName,
                request.MasterId,
                request.Statement);
            return Result<Forum_Section>.Success(newSection);
        }

        /// <summary>
        /// 创建Forum_Topic实体
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Result<Forum_Topic> CreateTopic(PostTopicRequest request) {
            // request合法性检查
            var validateRes = _postTopicRequestValidator.Validate(request);
            // request合法性检查未通过
            if (validateRes.IsValid is false) {
                return new Error(validateRes.Errors.ToString()!);
            }

            // 创建Forum_Topic实体
            var newTopic = Forum_Topic.Create(
                request.userId,
                request.sectionId,
                request.title,
                request.content);

            return Result<Forum_Topic>.Success(newTopic);
        }
    }
}
