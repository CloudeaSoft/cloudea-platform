using Cloudea.Domain.Forum.Entities.Recommend;
using Cloudea.Domain.Forum.Repositories.Recommend;
using Cloudea.Domain.Identity.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cloudea.Persistence.Repositories.Forum.Recommend
{
    public class UserPostInterestRepository(ApplicationDbContext context) : IUserPostInterestRepository
    {
        private readonly ApplicationDbContext _context = context;

        public void Add(UserPostInterest userPostInterest) =>
            _context.Add(userPostInterest);

        public void AddOrUpdateRange(ICollection<UserPostInterest> userPostInterests)
        {
            var existingKeys = _context.Set<UserPostInterest>()
                               .Select(x => new { x.UserId, x.PostId })
                               .ToList();

            // 用于批量插入的新记录列表  
            var newRecords = new List<UserPostInterest>();

            // 用于批量更新的现有记录列表  
            var recordsToUpdate = new List<UserPostInterest>();

            foreach (var item in userPostInterests)
            {
                var key = new
                {
                    item.UserId,
                    item.PostId
                };

                if (!existingKeys.Contains(key))
                {
                    // 如果键不存在于现有记录中，则添加到新记录列表  
                    newRecords.Add(item);
                }
                else
                {
                    // 如果键存在于现有记录中，则获取该记录并更新其值  
                    var exist = _context.Set<UserPostInterest>()
                        .FirstOrDefault(x =>
                            x.UserId == item.UserId &&
                            x.PostId == item.PostId);

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

        public async Task<List<UserPostInterest>> ListByPostIdAsync(
            Guid postId,
            CancellationToken cancellationToken = default) =>
            await _context.Set<UserPostInterest>()
                .Where(x => x.PostId == postId)
                .ToListAsync(cancellationToken);

        public async Task<List<UserPostInterest>> ListByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default) =>
            await _context.Set<UserPostInterest>()
                .Where(x => x.UserId == userId)
                .ToListAsync(cancellationToken);

    }
}
