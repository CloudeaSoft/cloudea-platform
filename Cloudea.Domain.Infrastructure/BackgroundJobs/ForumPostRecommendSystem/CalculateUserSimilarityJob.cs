using Cloudea.Domain.Common.Repositories;
using Cloudea.Domain.Forum.Entities.Recommend;
using Cloudea.Domain.Forum.Repositories.Recommend;
using Cloudea.Domain.Identity.Repositories;
using Quartz;
using System.Collections.Concurrent;

namespace Cloudea.Infrastructure.BackgroundJobs.ForumPostRecommendSystem;

public class CalculateUserSimilarityJob : IJob
{
    private readonly IUserPostInterestRepository _userPostInterestRepository;
    private readonly IUserSimilarityRepository _userSimilarityRepository;

    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    private const int MAX_COUNT = 20;

    public CalculateUserSimilarityJob(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IUserPostInterestRepository userPostInterestRepository,
        IUserSimilarityRepository userSimilarityRepository)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _userPostInterestRepository = userPostInterestRepository;
        _userSimilarityRepository = userSimilarityRepository;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        List<Guid> userIds = await _userRepository.ListAllUserIdAsync();

        foreach (Guid userId in userIds)
        {
            var similarities = await CalculateUserSimilarity(userId, userIds);

            _userSimilarityRepository.AddOrUpdateRange(similarities);
        }
        await _unitOfWork.SaveChangesAsync(context.CancellationToken);
    }

    private async Task<List<UserSimilarity>> CalculateUserSimilarity(Guid userId, List<Guid> relatedUserIds)
    {
        List<Guid> relatedUserIdsCopy = new(relatedUserIds);
        relatedUserIdsCopy.Remove(userId);
        List<UserSimilarity> result = [];

        foreach (var relatedUserId in relatedUserIdsCopy)
        {
            var userInterest = await _userPostInterestRepository.ListByUserIdAsync(userId);
            var relatedUserInterest = await _userPostInterestRepository.ListByUserIdAsync(relatedUserId);

            double score = CalculateInterestSimilarity(userInterest, relatedUserInterest);
            result.Add(UserSimilarity.Create(userId, relatedUserId, score));
        }

        return result.OrderByDescending(x => x.Score).Take(MAX_COUNT).ToList();
    }

    private double CalculateInterestSimilarity(
        List<UserPostInterest> userInterest,
        List<UserPostInterest> relatedUserInterest)
    {
        var userPostIds = userInterest.Select(i => i.PostId).ToHashSet();
        var relatedUserPostIds = relatedUserInterest.Select(i => i.PostId).ToHashSet();

        // 找到两个集合中都存在的PostId  
        var commonPostIds = userPostIds.Intersect(relatedUserPostIds);

        Dictionary<Guid, UserPostInterest> userInterestIntersectionDict = userInterest
            .Where(x => commonPostIds.Contains(x.PostId))
            .ToDictionary(i => i.PostId, i => i);

        Dictionary<Guid, UserPostInterest> relatedUserInterestIntersectionDict = relatedUserInterest
            .Where(x => commonPostIds.Contains(x.PostId))
            .ToDictionary(i => i.PostId, i => i);

        // result = a / (b^(1/2) * c^(1/2))
        ConcurrentBag<double> aBag = [];
        ConcurrentBag<double> bBag = [];
        ConcurrentBag<double> cBag = [];

        // 使用 Parallel.ForEach 来并行计算 a, b, c  
        Parallel.ForEach(commonPostIds, postId => {
            if (userInterestIntersectionDict.TryGetValue(postId, out var userItem) &&
                relatedUserInterestIntersectionDict.TryGetValue(postId, out var relatedUserItem))
            {
                double userScore = userItem.Score;
                double relatedUserScore = relatedUserItem.Score;

                aBag.Add(userScore * relatedUserScore);
                bBag.Add(userScore * userScore);
                cBag.Add(relatedUserScore * relatedUserScore);
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
