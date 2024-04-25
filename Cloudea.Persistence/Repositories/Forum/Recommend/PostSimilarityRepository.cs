using Cloudea.Domain.Forum.Entities.Recommend;
using Cloudea.Domain.Forum.Repositories.Recommend;
using Microsoft.EntityFrameworkCore;

namespace Cloudea.Persistence.Repositories.Forum.Recommend;

public class PostSimilarityRepository(ApplicationDbContext context) : IPostSimilarityRepository
{
    private readonly ApplicationDbContext _context = context;

    public void Add(PostSimilarity postSimilarity) =>
        _context.Add(postSimilarity);

    public void AddOrUpdateRange(ICollection<PostSimilarity> postSimilarities)
    {
        var existingKeys = _context.Set<PostSimilarity>()
                           .Select(x => new { x.PostId, x.RelatedPostId })
                           .ToList();

        // 用于批量插入的新记录列表  
        var newRecords = new List<PostSimilarity>();

        // 用于批量更新的现有记录列表  
        var recordsToUpdate = new List<PostSimilarity>();

        foreach (var item in postSimilarities)
        {
            var key = new
            {
                item.PostId,
                item.RelatedPostId
            };

            if (!existingKeys.Contains(key))
            {
                // 如果键不存在于现有记录中，则添加到新记录列表  
                newRecords.Add(item);
            }
            else
            {
                // 如果键存在于现有记录中，则获取该记录并更新其值  
                var exist = _context.Set<PostSimilarity>()
                    .FirstOrDefault(x =>
                        x.PostId == item.PostId &&
                        x.RelatedPostId == item.RelatedPostId);

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

    public async Task<List<PostSimilarity>> ListByPostIdAsync(
        Guid postId,
        CancellationToken cancellationToken = default) =>
        await _context.Set<PostSimilarity>()
            .Where(x => x.PostId == postId)
            .ToListAsync(cancellationToken);
}
