using Cloudea.Application.Abstractions;
using Cloudea.Application.Forum.Contracts.Response;
using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Forum.Repositories;
using Cloudea.Domain.Forum.Repositories.Recommend;
using Microsoft.Extensions.Caching.Memory;

namespace Cloudea.Application.Forum
{
    public class ForumRecommendService
    {
        private readonly ICurrentUser _currentUser;
        private readonly IUserPostInterestRepository _userPostInterestRepository;
        private readonly IUserSimilarityRepository _userSimilarityRepository;
        private readonly IPostSimilarityRepository _postSimilarityRepository;
        private readonly IForumPostRepository _forumPostRepository;

        private readonly IMemoryCache _memoryCache;

        private const int SIMILARITY_LIMIT = 10;
        private const int RECOMMEND_LIMIT = 90;
        private const int RECOMMEND_PAGE_LIMIT = 15;

        private readonly Func<Guid, string> CACHE_RECOMMEND_POST_RESULT = (Guid userId) => $"Forum:Recommend:{userId}";

        public ForumRecommendService(
            ICurrentUser currentUser,
            IUserPostInterestRepository userPostInterestRepository,
            IUserSimilarityRepository userSimilarityRepository,
            IPostSimilarityRepository postSimilarityRepository,
            IForumPostRepository forumPostRepository,
            IMemoryCache memoryCache)
        {
            _currentUser = currentUser;
            _userPostInterestRepository = userPostInterestRepository;
            _userSimilarityRepository = userSimilarityRepository;
            _postSimilarityRepository = postSimilarityRepository;
            _forumPostRepository = forumPostRepository;
            _memoryCache = memoryCache;
        }

        private async Task<List<Guid>> UserCFAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            // Find user - user to user
            var userSimilarities = await _userSimilarityRepository.ListByUserIdWithLimitAsync(userId, SIMILARITY_LIMIT, cancellationToken);
            var userIds = userSimilarities.Select(s => s.UserId).ToList();

            // Find user interest - user to item
            var interests = await _userPostInterestRepository.ListByUserIdListAsync(userIds, cancellationToken);

            return interests.Select(i => i.PostId).ToList();
        }

        private async Task<List<Guid>> ItemCFAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            // Find user interest - user to item
            var interests = await _userPostInterestRepository.ListByUserIdAsync(userId, cancellationToken);
            var postIds = interests.Select(x => x.PostId).ToList();

            // Find post - item to item
            var postSimilarities = await _postSimilarityRepository.ListByPostIdListWithLimitAsync(postIds, SIMILARITY_LIMIT * postIds.Count, cancellationToken);

            return postSimilarities.Select(x => x.PostId).ToList();
        }

        public async Task<Result<List<PostInfo>>> RecommendPostAsync(CancellationToken cancellationToken = default)
        {
            var userId = await _currentUser.GetUserIdAsync(cancellationToken);

            if (userId == Guid.Empty)
            {
                var randomPostList = await _forumPostRepository.ListRandomPostWithLimitAsync(RECOMMEND_PAGE_LIMIT, cancellationToken);

                return randomPostList.Select(PostInfo.Create).OrderBy(x => Guid.NewGuid()).ToList();
            }

            if (_memoryCache.TryGetValue(CACHE_RECOMMEND_POST_RESULT(userId), out List<PostInfo>? memory))
            {
                if (memory is not null && memory.Count >= RECOMMEND_PAGE_LIMIT)
                {
                    var resultRes = memory.Take(RECOMMEND_PAGE_LIMIT).ToList();
                    var cacheRes = memory.Skip(RECOMMEND_PAGE_LIMIT).ToList();

                    _memoryCache.Set<List<PostInfo>>(CACHE_RECOMMEND_POST_RESULT(userId), cacheRes, TimeSpan.FromDays(1));

                    return resultRes;
                }
            }

            var itemCFResult = await ItemCFAsync(userId, cancellationToken);
            var userCFResult = await UserCFAsync(userId, cancellationToken);

            var postIds = itemCFResult.Concat(userCFResult).Distinct().ToList();
            var postList = await _forumPostRepository.ListByPostIdListAsync(postIds, cancellationToken);

            if (postList.Count < RECOMMEND_LIMIT)
            {
                var randomPostList = await _forumPostRepository.ListRandomPostWithLimitAsync(RECOMMEND_LIMIT - postList.Count, cancellationToken);
                postList.AddRange(randomPostList);
            }

            List<PostInfo> recommendRes = postList.Select(PostInfo.Create).OrderBy(x => Guid.NewGuid()).ToList();

            // 拆分列表  
            List<PostInfo> resultList = recommendRes.Take(RECOMMEND_PAGE_LIMIT).ToList();
            List<PostInfo> cacheList = recommendRes.Skip(RECOMMEND_PAGE_LIMIT).ToList();

            _memoryCache.Set(CACHE_RECOMMEND_POST_RESULT(userId), cacheList, TimeSpan.FromDays(1));

            return resultList;
        }
    }
}
