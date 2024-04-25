using Cloudea.Domain.Common.Repositories;
using Cloudea.Domain.Forum.Entities.Recommend;
using Cloudea.Domain.Forum.Repositories;
using Cloudea.Domain.Forum.Repositories.Recommend;
using Quartz;
using System.Collections.Concurrent;

namespace Cloudea.Infrastructure.BackgroundJobs.ForumPostRecommendSystem;

public class CalculatePostSimilarityJob : IJob
{
    private readonly IUserPostInterestRepository _userPostInterestRepository;
    private readonly IPostSimilarityRepository _postSimilarityRepository;

    private readonly IForumPostRepository _forumPostRepository;
    private readonly IUnitOfWork _unitOfWork;

    private const int MAX_COUNT = 20;

    public CalculatePostSimilarityJob(
        IUserPostInterestRepository userPostInterestRepository,
        IPostSimilarityRepository postSimilarityRepository,
        IForumPostRepository forumPostRepository,
        IUnitOfWork unitOfWork)
    {
        _userPostInterestRepository = userPostInterestRepository;
        _postSimilarityRepository = postSimilarityRepository;
        _forumPostRepository = forumPostRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        List<Guid> postIds = await _forumPostRepository.ListAllPostIdAsync();

        foreach (Guid postId in postIds)
        {
            var similarities = await CalculatePostSimilarity(postId, postIds);

            _postSimilarityRepository.AddOrUpdateRange(similarities);
        }

        await _unitOfWork.SaveChangesAsync(context.CancellationToken);
    }

    private async Task<List<PostSimilarity>> CalculatePostSimilarity(Guid postId, List<Guid> relatedPostIds)
    {
        List<Guid> relatedPostIdsCopy = new(relatedPostIds);
        relatedPostIdsCopy.Remove(postId);
        List<PostSimilarity> result = [];

        foreach (var relatedPostId in relatedPostIdsCopy)
        {
            var postAudience = await _userPostInterestRepository.ListByPostIdAsync(postId);
            var relatedPostAudience = await _userPostInterestRepository.ListByPostIdAsync(relatedPostId);

            double score = CalculateAudienceSimilarity(postAudience, relatedPostAudience);
            result.Add(PostSimilarity.Create(postId, relatedPostId, score));
        }

        return result.OrderByDescending(x => x.Score).Take(MAX_COUNT).ToList();
    }

    private double CalculateAudienceSimilarity(
        List<UserPostInterest> postAudience,
        List<UserPostInterest> relatedPostAudience)
    {
        var postUserIds = postAudience.Select(i => i.UserId).ToHashSet();
        var relatedPostUserIds = relatedPostAudience.Select(i => i.UserId).ToHashSet();

        // 找到两个集合中都存在的UserId  
        var commonUserIds = postUserIds.Intersect(relatedPostUserIds);

        Dictionary<Guid, UserPostInterest> postAudienceIntersectionDict = postAudience
            .Where(x => commonUserIds.Contains(x.UserId))
            .ToDictionary(i => i.UserId, i => i);

        Dictionary<Guid, UserPostInterest> relatedPostAudienceIntersectionDict = relatedPostAudience
            .Where(x => commonUserIds.Contains(x.UserId))
            .ToDictionary(i => i.UserId, i => i);

        // result = a / (b^(1/2) * c^(1/2))
        ConcurrentBag<double> aBag = [];
        ConcurrentBag<double> bBag = [];
        ConcurrentBag<double> cBag = [];

        // 使用 Parallel.ForEach 来并行计算 a, b, c  
        Parallel.ForEach(commonUserIds, userId => {
            if (postAudienceIntersectionDict.TryGetValue(userId, out var postItem) &&
                relatedPostAudienceIntersectionDict.TryGetValue(userId, out var relatedItem))
            {
                double postScore = postItem.Score;
                double relatedScore = relatedItem.Score;

                aBag.Add(postScore * relatedScore);
                bBag.Add(postScore * postScore);
                cBag.Add(relatedScore * relatedScore);
            }
        });

        double a = aBag.Sum();
        double b = bBag.Sum();
        double c = cBag.Sum();

        if (b > 0 && c > 0)
        {
            double numerator = a;
            double denominator = Math.Sqrt(b * c);
            return numerator / denominator;
        }
        else
        {
            return 0;
        }
    }
}
