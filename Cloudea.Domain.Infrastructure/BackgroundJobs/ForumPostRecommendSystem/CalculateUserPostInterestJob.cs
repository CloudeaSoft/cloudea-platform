using Cloudea.Domain.Common.Repositories;
using Cloudea.Domain.Forum.Entities.Recommend;
using Cloudea.Domain.Forum.Repositories;
using Cloudea.Domain.Forum.Repositories.Recommend;
using Cloudea.Domain.Identity.Repositories;
using Cloudea.Persistence;
using Quartz;

namespace Cloudea.Infrastructure.BackgroundJobs.ForumPostRecommendSystem;

public class CalculateUserPostInterestJob : IJob
{
    private readonly IForumPostUserHistoryRepository _forumPostUserHistoryRepository;
    private readonly IForumPostUserLikeRepository _forumPostUserLikeRepository;
    private readonly IForumPostUserFavoriteRepository _forumPostUserFavoriteRepository;

    private readonly IUserPostInterestRepository _userPostInterestRepository;

    private readonly IUserRepository _userRepository;

    private readonly IUnitOfWork _unitOfWork;

    public CalculateUserPostInterestJob(
        IForumPostUserHistoryRepository forumPostUserHistoryRepository,
        IUserRepository userRepository,
        IForumPostUserLikeRepository forumPostUserLikeRepository,
        IForumPostUserFavoriteRepository forumPostUserFavoriteRepository,
        IUserPostInterestRepository userPostInterestRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;

        _forumPostUserHistoryRepository = forumPostUserHistoryRepository;
        _forumPostUserLikeRepository = forumPostUserLikeRepository;
        _forumPostUserFavoriteRepository = forumPostUserFavoriteRepository;
        _userPostInterestRepository = userPostInterestRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        // Retrieve a list of all user IDs  
        var userIdList = await _userRepository.ListAllUserIdAsync();

        // Iterate over each user ID  
        foreach (var userId in userIdList)
        {
            var userLikePostList = await _forumPostUserLikeRepository.ListByUserIdAsync(userId, context.CancellationToken);
            var userFavoritePostIdList = await _forumPostUserFavoriteRepository.ListPostIdByUserIdAsync(userId, context.CancellationToken);
            var userHistoryPostIdList = await _forumPostUserHistoryRepository.ListPostIdByUserIdAsync(userId, context.CancellationToken);

            // onl
            var postIdList = userHistoryPostIdList.ToList();

            // Initialize the list to store user's post interests  
            var postInterestList = new List<UserPostInterest>();

            // Iterate over each post ID  
            foreach (var postId in postIdList)
            {
                // Initialize the score for the current post  
                double score = 0.2;

                // Check if the user liked the post  
                var like = userLikePostList.FirstOrDefault(x => x.ParentPostId == postId);
                if (like is not null && like.IsLike)
                {
                    score += 0.4;
                }

                // Check if the post is in the user's favorites  
                if (userFavoritePostIdList.Where(x => x == postId).Any())
                {
                    score += 0.4;
                }

                var interest = UserPostInterest.Create(userId, postId, score);
                postInterestList.Add(interest);
            }

            // Sort the post interests by score in descending order and take the top 10  
            if (postInterestList.Count > 10)
                postInterestList = postInterestList
                    .OrderByDescending(x => x.Score)
                    .Take(10)
                    .ToList();

            // Add the user's post interests to the repository  
            _userPostInterestRepository.AddOrUpdateRange(postInterestList);
        }

        // Save all changes to the database  
        await _unitOfWork.SaveChangesAsync(context.CancellationToken);
    }
}
