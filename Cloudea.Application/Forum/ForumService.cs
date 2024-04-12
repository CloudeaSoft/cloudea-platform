using Cloudea.Application.Abstractions;
using Cloudea.Application.Forum.Contracts;
using Cloudea.Domain.Common.Repositories;
using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Forum.Entities;
using Cloudea.Domain.Forum.Repositories;
using Cloudea.Domain.Identity.Repositories;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cloudea.Application.Forum
{
    /// <summary>
    /// Forum模块
    /// </summary>
    public class ForumService(
        IForumPostRepository forumPostRepository,
        IForumSectionRepository forumSectionRepository,
        IUnitOfWork unitOfWork,
        IForumReplyRepository forumReplyRepository,
        IUserRepository userRepository,
        ICurrentUser currentUser,
        IForumCommentRepository forumCommentRepository,
        IForumPostUserHistoryRepository forumPostUserHistoryRepository,
        ILogger<ForumService> logger,
        IForumPostUserLikeRepository forumPostUserLikeRepository,
        IForumPostUserFavoriteRepository forumPostUserFavoriteRepository,
        IUserProfileRepository userProfileRepository)
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICurrentUser _currentUser = currentUser;
        private readonly IUserProfileRepository _userProfileRepository = userProfileRepository;

        private readonly IForumPostRepository _forumPostRepository = forumPostRepository;
        private readonly IForumPostUserHistoryRepository _forumPostUserHistoryRepository = forumPostUserHistoryRepository;
        private readonly IForumPostUserLikeRepository _forumPostUserLikeRepository = forumPostUserLikeRepository;
        private readonly IForumPostUserFavoriteRepository _forumPostUserFavoriteRepository = forumPostUserFavoriteRepository;

        private readonly IForumSectionRepository _forumSectionRepository = forumSectionRepository;
        private readonly IForumReplyRepository _forumReplyRepository = forumReplyRepository;
        private readonly IForumCommentRepository _forumCommentRepository = forumCommentRepository;

        private readonly ILogger<ForumService> _logger = logger;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        /// <summary>
        /// 创建主题
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Result<Guid>> CreateSectionAsync(
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
            var section = await _forumSectionRepository.GetByIdAsync(id, cancellationToken);
            if (section is null) {
                return new Error("ForumSection.NotFound");
            }

            section.Update(request.Name, request.Statement, request.MasterId);

            _forumSectionRepository.Update(section);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        /// <summary>
        /// 查看主题内容
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Result<ForumSection>> GetSectionAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var section = await _forumSectionRepository.GetByIdAsync(id, cancellationToken);
            if (section is null) {
                return new Error("ForumSection.NotFound");
            }

            return section;
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
            var pageResponse = await _forumSectionRepository.GetWithPageRequestAsync(request, cancellationToken);
            if (pageResponse.Rows.Count <= 0) {
                return new Error("ForumSection.NotFound");
            }

            return pageResponse;
        }

        /// <summary>
        /// 创建主题帖
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Result<Guid?>> CreatePostAsync(
            Guid userId,
            CreatePostRequest request,
            CancellationToken cancellationToken = default)
        {
            var section = await _forumSectionRepository.GetByIdAsync(request.SectionId, cancellationToken);
            if (section is null) {
                return new Error("ForumSection.NotFound",
                    $"The forum_section with Id {request.SectionId} was not found");
            }

            var newPost = section.AddPost(
                userId,
                request.Title,
                request.Content);
            if (newPost is null) {
                return new Error("ForumPost.InvaildParameter");
            }

            _forumPostRepository.Add(newPost);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return newPost.Id;
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
                return new Error("ForumPost.NotFound");
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
            // Get Post / If null return error
            var post = await _forumPostRepository.GetByIdAsync(postId, cancellationToken);
            if (post is null) {
                return new Error("ForumPost.NotFound");
            }

            try {
                // Create response instance
                var postInfo = PostInfo.Create(post);
                var response = GetPostInfoResponse.Create(postInfo);

                // Get Replys
                var replys = await _forumReplyRepository.GetByPostIdWithPageRequestAsync(postId, request, cancellationToken);
                if (replys is null || replys.Rows is null || replys.Rows.Count <= 0) {
                    return response;
                }

                // Get Comments
                var replyIds = replys.Rows.Select(x => x.Id).ToList();
                var comments = await _forumCommentRepository.ListByReplyIdsAsync(replyIds, cancellationToken);

                // Fill Replys with their Comments
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
            catch (NullReferenceException ex) {
                _logger.LogCritical(ex.ToString());
                throw;
            }
            finally {
                // Create user history before return
                var userId = await _currentUser.GetUserIdAsync();
                if (userId == Guid.Empty) {
                    // Create guest history
                }
                else {
                    var history = post.CreateHistory(userId)!;
                    _forumPostUserHistoryRepository.Add(history);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }
            }
        }

        public async Task<Result<Guid>> CreateLikeOnPostAsync(Guid postId, CancellationToken cancellationToken = default)
        {
            // Get Post / If null return error
            var post = await _forumPostRepository.GetByIdAsync(postId, cancellationToken);
            if (post is null) {
                return new Error("ForumPost.NotFound");
            }

            var userId = await _currentUser.GetUserIdAsync();

            var like = await _forumPostUserLikeRepository.GetByUserIdPostIdAsync(userId, postId);
            if (like is not null) {
                return new Error("ForumPostUserLike.Exist");
            }

            var newLike = post.CreateLike(userId)!;
            _forumPostUserLikeRepository.Add(newLike);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return newLike.Id;
        }

        public async Task<Result> DeleteLikeOnPostAsync(Guid postId, CancellationToken cancellationToken = default)
        {
            // Get Post / If null return error
            var post = await _forumPostRepository.GetByIdAsync(postId, cancellationToken);
            if (post is null) {
                return new Error("ForumPost.NotFound");
            }

            var userId = await _currentUser.GetUserIdAsync();

            var like = await _forumPostUserLikeRepository.GetByUserIdPostIdAsync(userId, postId, cancellationToken);
            if (like is null) {
                return new Error("ForumPostUserLike.NotFound");
            }

            _forumPostUserLikeRepository.Delete(like);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result<Guid>> CreateDislikeOnPostAsync(Guid postId, CancellationToken cancellationToken = default)
        {
            // Get Post / If null return error
            var post = await _forumPostRepository.GetByIdAsync(postId, cancellationToken);
            if (post is null) {
                return new Error("ForumPost.NotFound");
            }

            var userId = await _currentUser.GetUserIdAsync();

            var like = await _forumPostUserLikeRepository.GetByUserIdPostIdAsync(userId, postId, cancellationToken);
            if (like is not null) {
                return new Error("ForumPostUserLike.Exist");
            }

            var dislike = post.CreateDislike(userId)!;
            _forumPostUserLikeRepository.Add(dislike);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return dislike.Id;
        }

        public async Task<Result<Guid>> CreateFavoriteOnPostAsync(Guid postId, CancellationToken cancellationToken = default)
        {
            // Get Post / If null return error
            var post = await _forumPostRepository.GetByIdAsync(postId, cancellationToken);
            if (post is null) {
                return new Error("ForumPost.NotFound");
            }

            var userId = await _currentUser.GetUserIdAsync();
            var favorite = post.CreateFavorite(userId)!;
            _forumPostUserFavoriteRepository.Add(favorite);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return favorite.Id;
        }

        public async Task<Result> DeleteFavoriteOnPostAsync(Guid postId, CancellationToken cancellationToken = default)
        {
            // Get Post / If null return error
            var post = await _forumPostRepository.GetByIdAsync(postId, cancellationToken);
            if (post is null) {
                return new Error("ForumPost.NotFound");
            }

            var userId = await _currentUser.GetUserIdAsync();

            var favorite = await _forumPostUserFavoriteRepository.GetByUserIdPostIdAsync(userId, postId, cancellationToken);
            if (favorite is null) {
                return new Error("ForumPostUserLike.NotFound");
            }

            _forumPostUserFavoriteRepository.Delete(favorite);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        /// <summary>
        /// 查看主题帖列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Result<ListPostResponse>> ListPostAsync(
            PageRequest request,
            Guid? sectionId,
            CancellationToken cancellationToken = default)
        {
            var list = await _forumPostRepository.GetWithPageRequestSectionIdAsync(request, sectionId, cancellationToken);
            var userIdList = list.Rows.Select(x => x.OwnerUserId).ToList();
            var userProfileList = await _userProfileRepository.ListByUserIdAsync(userIdList, cancellationToken);

            var response = ListPostResponse.Create(list);
            foreach (var item in response.Rows) {
                item.OwnerUser = userProfileList.Where(x => x.Id == item.OwnerUserId).FirstOrDefault()!;
            }

            return response;
        }


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

            var reply = post.CreateReply(userId, content);
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

            var comment = reply.AddComment(ownerUserId, content, targetUserId);
            if (comment is null) {
                return new Error("ForumComment.InvalidParam");
            }

            _forumCommentRepository.Add(comment);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return comment.Id;
        }
    }
}
