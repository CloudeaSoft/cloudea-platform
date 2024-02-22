using Cloudea.Entity.Forum;
using Cloudea.Infrastructure.Models;
using Cloudea.Service.Forum.Domain.Models;
using Cloudea.Service.Forum.Domain.Repositories;

namespace Cloudea.Service.Forum.Domain
{
    /// <summary>
    /// Forum模块 的功能集合类
    /// </summary>
    public class ForumApplicationService(
        ForumDomainService domainService,
        IForumTopicRepository forumTopicRepository,
        IForumSectionRepository forumSectionRepository,
        IForumRepository domainRepository) {
        /// <summary>
        /// Fields
        /// </summary>
        private readonly ForumDomainService _domainService = domainService;
        private readonly IForumTopicRepository _forumTopicRepository = forumTopicRepository;
        private readonly IForumSectionRepository _forumSectionRepository = forumSectionRepository;
        private readonly IForumRepository _domainRepository = domainRepository;

        /// <summary>
        /// 创建主题
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Result<long>> PostSectionAsync(PostSectionRequest request) {
            var createRes = _domainService.CreateSection(request);
            if (createRes.IsFailure) {
                return createRes.Error;
            }
            return await _forumSectionRepository.Save(createRes.Data);
        }

        /// <summary>
        /// 更新主题
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Result<List<Forum_Section>>> UpdateSectionAsync(List<UpdateSectionRequest> request) {
            // 1. 查询request对应的实体列表
            var list = request.GetIdList();
            var oldSectionList = await _forumSectionRepository.List(list);
            // 2. 根据request内容修改实体列表
            var newSectionList = _domainService.UpdateSectionList(oldSectionList.Data, request);
            // 3. 调用Repository进行实体更新
            return await _forumSectionRepository.Update(newSectionList.Data);
        }

        /// <summary>
        /// 查看主题内容
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Result<Forum_Section>> GetSectionAsync(Guid id) {
            return await _forumSectionRepository.Read(id);
        }

        /// <summary>
        /// 查看主题列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Result<PageResponse<Forum_Section>>> ListSectionAsync(PageRequest request) {
            return await _forumSectionRepository.List(request);
        }

        /// <summary>
        /// 创建主题帖
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Result<Forum_Topic>> PostTopicAsync(PostTopicRequest request) {
            var createRes = _domainService.CreateTopic(request);
            if (createRes.IsFailure) {
                return createRes.Error;
            }
            return await _domainRepository.SaveTopicAsync(createRes.Data);
        }

        /// <summary>
        /// 查看主题帖内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<Forum_Topic>> GetTopicAsync(Guid id) {
            return await _forumTopicRepository.Read(id);
        }

        /// <summary>
        /// 查看主题帖列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Result<PageResponse<Forum_Topic>>> ListTopicAsync(PageRequest request) {
            return await _forumTopicRepository.List(request);
        }
    }
}
