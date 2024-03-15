using Cloudea.Application.Forum.Contracts;
using Cloudea.Domain.Common.Shared;
using Cloudea.Infrastructure.Repositories;
using Cloudea.Infrastructure.Shared;
using Cloudea.Service.Auth.Domain.Abstractions;
using Cloudea.Service.Auth.Domain.Repositories;
using Cloudea.Service.Forum.Domain.Entities;
using Cloudea.Service.Forum.Domain.Repositories;

namespace Cloudea.Application.Forum
{
    /// <summary>
    /// Forum模块 的功能集合类
    /// </summary>
    public class ForumService(
        IForumPostRepository forumPostRepository,
        IForumSectionRepository forumSectionRepository,
        IUnitOfWork unitOfWork,
        IForumReplyRepository forumReplyRepository,
        IUserRepository userRepository,
        ICurrentUser currentUser,
        IForumCommentRepository forumCommentRepository)
    {
        /// <summary>
        /// Fields
        /// </summary>
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICurrentUser _currentUser = currentUser;

        private readonly IForumPostRepository _forumPostRepository = forumPostRepository;
        private readonly IForumSectionRepository _forumSectionRepository = forumSectionRepository;
        private readonly IForumReplyRepository _forumReplyRepository = forumReplyRepository;
        private readonly IForumCommentRepository _forumCommentRepository = forumCommentRepository;
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
            var master = (await _userRepository.GetByIdAsync(request.MasterId));

            if (master is null) {
                return new Error("User.NotFound");
            }

            var newSection = ForumSection.Create(
                request.SectionName,
                master.Id,
                request.Statement);

            if (newSection is null) {
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
            if (section is null) {
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
        public async Task<Result<ForumPost?>> PostPostAsync(
            Guid userId,
            CreateTopicRequest request,
            CancellationToken cancellationToken = default)
        {
            var section = await _forumSectionRepository.GetByIdAsync(request.SectionId);

            if (section is null) {
                return new Error("ForumSection.NotFound",
                    $"The forum_section with Id {request.SectionId} was not found");
            }

            var newTopic = ForumPost.Create(
                userId,
                section,
                request.Title,
                request.Content);

            if (newTopic is null) {
                return new Error("ForumTopic.InvaildParameter");
            }

            // 先查询 逻辑修改
            section.IncreaseTopicCount();

            // 更新
            _forumPostRepository.Add(newTopic);
            _forumSectionRepository.Update(section);

            // 保存
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // 返回结果
            return await _forumPostRepository.GetByIdAsync(newTopic.Id, cancellationToken);
        }

        /// <summary>
        /// 查看主题帖内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<ForumPost?>> GetPostAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var res = await _forumPostRepository.GetByIdAsync(id, cancellationToken);

            if (res is null) {
                return new Error("ForumTopic.NotFound");
            }

            return res;
        }

        /// <summary>
        /// 查看主题帖的一页回复贴与其评论
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<GetPostInfoResponse>> GetPostInfoAsync(
            Guid postId,
            PageRequest request,
            CancellationToken cancellationToken = default)
        {
            // 获取Post
            var post = await _forumPostRepository.GetByIdAsync(postId, cancellationToken);
            if (post is null) {
                return new Error("ForumTopic.NotFound");
            }

            // 创建返回结果
            var postInfo = PostInfo.Create(post);
            var response = GetPostInfoResponse.Create(postInfo);

            // 获取Reply
            var replys = await _forumReplyRepository.GetByTopicIdWithPageRequestAsync(postId, request, cancellationToken);
            if (replys is null) {
                return response;
            }

            // 获取Comments
            var replyIds = replys.Rows.Select(x => x.Id).ToList();
            var comments = await _forumCommentRepository.ListByReplyIdsAsync(replyIds, cancellationToken);

            // 填充Reply结果
            PageResponse<ReplyInfo> replyInfos = new() {
                Total = replys.Total,
                Rows = []
            };
            foreach (var reply in replys.Rows) {
                var commentInfos = new List<CommentInfo>();
                foreach (var comment in comments.Where(x => x.ParentReplyId == reply.Id)) {
                    commentInfos.Add(CommentInfo.Create(comment));
                }
                replyInfos.Rows.Add(ReplyInfo.Create(reply, commentInfos));
            }

            return GetPostInfoResponse.Create(postInfo, replyInfos);
        }

        /// <summary>
        /// 查看主题帖列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Result<PageResponse<ForumPost>>> ListTopicAsync(
            PageRequest request,
            CancellationToken cancellationToken = default) =>
            await _forumPostRepository.GetWithPageRequestAsync(request, cancellationToken);

        /// <summary>
        /// 创建主题帖回帖
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<Guid>> PostReplyAsync(Guid postId, string content, CancellationToken cancellationToken = default)
        {
            var userId = await _currentUser.GetUserIdAsync();

            var post = await _forumPostRepository.GetByIdAsync(postId, cancellationToken);
            if (post is null) {
                return new Error("ForumPost.NotFound");
            }

            var reply = ForumReply.Create(userId, post, content);
            if (reply is null) {
                return new Error("ForumReply.InvalidParam");
            }

            _forumReplyRepository.Add(reply);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return reply.Id;
        }

        public async Task<Result<Guid>> PostCommentAsync(Guid replyId, Guid? targetUserId, string content, CancellationToken cancellationToken = default)
        {
            var ownerUserId = await _currentUser.GetUserIdAsync();

            var reply = await _forumReplyRepository.GetByIdAsync(replyId, cancellationToken);
            if (reply is null) {
                return new Error("ForumReply.NotFound");
            }

            var comment = ForumComment.Create(reply, ownerUserId, targetUserId, content);
            if (comment is null) {
                return new Error("ForumComment.InvalidParam");
            }

            _forumCommentRepository.Add(comment);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return comment.Id;
        }
    }
}
