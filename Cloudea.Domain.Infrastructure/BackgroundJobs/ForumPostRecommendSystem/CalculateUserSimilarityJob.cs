using Cloudea.Domain.Common.Repositories;
using Cloudea.Domain.Forum.Entities.Recommend;
using Cloudea.Domain.Forum.Repositories.Recommend;
using Cloudea.Domain.Identity.Repositories;
using Quartz;

namespace Cloudea.Infrastructure.BackgroundJobs.ForumPostRecommendSystem;

public class CalculateUserSimilarityJob : IJob
{
    private readonly IUserPostInterestRepository _userPostInterestRepository;

    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    private const int MAX_COUNT = 20;

    public CalculateUserSimilarityJob(IUserRepository userRepository, IUnitOfWork unitOfWork, IUserPostInterestRepository userPostInterestRepository)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _userPostInterestRepository = userPostInterestRepository;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        ICollection<Guid> userIds = await _userRepository.ListAllUserIdAsync();

        foreach (Guid userId in userIds)
        {
            var similarities = await CalculateUserSimilarity(userId, userIds);
        }
    }

    private async Task<ICollection<(Guid, double)>> CalculateUserSimilarity(Guid userId, ICollection<Guid> relatedUserIds)
    {
        relatedUserIds.Remove(userId);
        ICollection<(Guid, double)> result = [];

        foreach (var relatedUserId in relatedUserIds)
        {
            var userInterest = await _userPostInterestRepository.ListByUserIdAsync(userId);
            var relatedUserInterest = await _userPostInterestRepository.ListByUserIdAsync(relatedUserId);

            double score = await CalculateInterestSimilarity(userInterest, relatedUserInterest);
            result.Add((relatedUserId, score));
        }

        return result.OrderByDescending(x => x.Item2).Take(MAX_COUNT).ToList();
    }

    private async Task<double> CalculateInterestSimilarity(
        ICollection<UserPostInterest> userInterest,
        ICollection<UserPostInterest> relatedUserInterest)
    {
        var userPostIds = userInterest.Select(i => i.PostId).ToHashSet();
        var relatedUserPostIds = relatedUserInterest.Select(i => i.PostId).ToHashSet();

        // 找到两个集合中都存在的PostId  
        var commonPostIds = userPostIds.Intersect(relatedUserPostIds);

        ICollection<UserPostInterest> userInterestIntersection =
            userInterest
            .Where(x => commonPostIds.Contains(x.PostId)).ToList();

        ICollection<UserPostInterest> relatedUserInterestIntersection =
            relatedUserInterest
            .Where(x => commonPostIds.Contains(x.PostId)).ToList();

        // result = a / (b^(1/2) * c^(1/2))
        double a = 0;
        double b = 0;
        double c = 0;

        await Task.Run(() => {
            foreach (var item in userInterestIntersection)
            {
                var itemB = relatedUserInterestIntersection
                    .Where(x => x.PostId == item.PostId)
                    .FirstOrDefault();

                if (itemB != null)
                {
                    a += item.Score * itemB.Score;
                    b += item.Score * item.Score;
                }
            }

            foreach (var item in relatedUserInterestIntersection)
            {
                c += item.Score * item.Score;
            }
        });

        if (b > 0 && c > 0)
        {
            double sqrtB = Math.Sqrt(b);
            double sqrtC = Math.Sqrt(c);
            return a / (sqrtB * sqrtC);
        }
        else
        {
            return 0;
        }
    }
}
