using Cloudea.Domain.Forum.Entities.Recommend;
using Cloudea.Domain.Forum.Repositories.Recommend;
using Microsoft.EntityFrameworkCore;

namespace Cloudea.Persistence.Repositories.Forum.Recommend;


public class UserSimilarityRepository(ApplicationDbContext context) : IUserSimilarityRepository
{
    private readonly ApplicationDbContext _context = context;

    public void Add(UserSimilarity userSimilarity) =>
        _context.Add(userSimilarity);

    public void AddOrUpdateRange(ICollection<UserSimilarity> userSimilarities)
    {
        var existingKeys = _context.Set<UserSimilarity>()
                           .Select(x => new { x.UserId, x.RelatedUserId })
                           .ToList();

        // 用于批量插入的新记录列表  
        var newRecords = new List<UserSimilarity>();

        // 用于批量更新的现有记录列表  
        var recordsToUpdate = new List<UserSimilarity>();

        foreach (var item in userSimilarities)
        {
            var key = new
            {
                item.UserId,
                item.RelatedUserId
            };

            if (!existingKeys.Contains(key))
            {
                // 如果键不存在于现有记录中，则添加到新记录列表  
                newRecords.Add(item);
            }
            else
            {
                // 如果键存在于现有记录中，则获取该记录并更新其值  
                var exist = _context.Set<UserSimilarity>()
                    .FirstOrDefault(x =>
                        x.UserId == item.UserId &&
                        x.RelatedUserId == item.RelatedUserId);

                if (exist is not null)
                {
                    exist.Score = item.Score;
                    recordsToUpdate.Add(exist); // 添加到列表以标记为已修改  
                }
            }
        }

        // 批量插入新记录  
        _context.AddRange(newRecords);

        _context.UpdateRange(recordsToUpdate);
    }

    public async Task<List<UserSimilarity>> ListByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default) =>
        await _context.Set<UserSimilarity>()
            .Where(x => x.UserId == userId)
            .ToListAsync(cancellationToken);
}
