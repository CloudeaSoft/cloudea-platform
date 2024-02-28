using Cloudea.Application.Contracts;
using Cloudea.Infrastructure.Repositories;
using Cloudea.Infrastructure.Shared;
using Cloudea.Service.Auth.Domain.Abstractions;
using Cloudea.Service.Auth.Domain.Repositories;
using Cloudea.Service.Forum.Domain.Entities;
using Cloudea.Service.Forum.Domain.Models;
using Cloudea.Service.Forum.Domain.Repositories;

namespace Cloudea.Application.Forum
{
    /// <summary>
    /// Forum模块 的功能集合类
    /// </summary>
    public class ForumService(
        IForumPostRepository forumTopicRepository,
        IForumSectionRepository forumSectionRepository,
        IUnitOfWork unitOfWork,
        IForumReplyRepository forumReplyRepository,
        IUserRepository userRepository,
        ICurrentUser currentUser)
    {
        /// <summary>
        /// Fields
        /// </summary>
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICurrentUser _currentUser = currentUser;

        private readonly IForumPostRepository _forumTopicRepository = forumTopicRepository;
        private readonly IForumSectionRepository _forumSectionRepository = forumSectionRepository;
        private readonly IForumReplyRepository _forumReplyRepository = forumReplyRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        /// <summary>
        /// 创建主题
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Result<Guid>> PostSectionAsync(
            CreateSectionRequest request,
            CancellationToken cancellationToken = default)
        {
            var master = (await _userRepository.GetUser(request.MasterId)).Data;

            if (master == null)
            {
                return new Error("User.NotFound");
            }

            var newSection = ForumSection.Create(
                request.SectionName,
                master,
                request.Statement);

            if (newSection is null)
            {
                return new Error("ForumSection.InvaildParameter");
            }

            _forumSectionRepository.Add(newSection);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return newSection.Id;
        }

        /// <summary>
        /// 更新主题
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Result> UpdateSectionAsync(
            Guid id,
            UpdateSectionRequest request,
            CancellationToken cancellationToken = default)
        {
            // 1. 查询request对应的实体列表
            var section = await _forumSectionRepository.GetByIdAsync(id, cancellationToken);
            if (section is null)
            {
                return new Error("ForumSection.NotFound");
            }

            // 2. 根据request内容修改实体列表
            section.Update(request.Name, request.Statement, request.MasterId);

            // 3. 调用Repository进行实体更新
            _forumSectionRepository.Update(section);

            // 4. 保存修改
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        /// <summary>
        /// 查看主题内容
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Result<ForumSection?>> GetSectionAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _forumSectionRepository.GetByIdAsync(id, cancellationToken);
        }

        /// <summary>
        /// 查看主题列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Result<PageResponse<ForumSection>>> ListSectionAsync(
            PageRequest request,
            CancellationToken cancellationToken = default)
        {
            return await _forumSectionRepository.GetWithPageRequestAsync(request, cancellationToken);
        }

        /// <summary>
        /// 创建主题帖
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Result<ForumPost?>> PostTopicAsync(
            Guid userId,
            CreateTopicRequest request,
            CancellationToken cancellationToken = default)
        {
            var section = await _forumSectionRepository.GetByIdAsync(request.SectionId);

            if (section is null)
            {
                return new Error("ForumSection.NotFound",
                    $"The forum_section with Id {request.SectionId} was not found");
            }

            var newTopic = ForumPost.Create(
                userId,
                section,
                request.Title,
                request.Content);

            if (newTopic is null)
            {
                return new Error("ForumTopic.InvaildParameter");
            }

            // 先查询 逻辑修改
            section.IncreaseTopicCount();

            // 更新
            _forumTopicRepository.Add(newTopic);
            _forumSectionRepository.Update(section);

            // 保存
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // 返回结果
            return await _forumTopicRepository.GetByIdAsync(newTopic.Id, cancellationToken);
        }

        /// <summary>
        /// 查看主题帖内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<ForumPost?>> GetTopicAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var res = await _forumTopicRepository.GetByIdAsync(id, cancellationToken);

            if (res is null)
            {
                return new Error("ForumTopic.NotFound");
            }

            return res;
        }


        /// <summary>
        /// 查看主题帖列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Result<PageResponse<ForumPost>>> ListTopicAsync(
            PageRequest request,
            CancellationToken cancellationToken = default) =>
            await _forumTopicRepository.GetWithPageRequestAsync(request, cancellationToken);

        /// <summary>
        /// 创建主题帖回帖
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<Guid>> PostReplyAsync(ForumReply reply, CancellationToken cancellationToken = default)
        {


            _forumReplyRepository.Add(reply);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return reply.Id;
        }
    }
}
