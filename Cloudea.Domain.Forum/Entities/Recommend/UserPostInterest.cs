using Cloudea.Domain.Common.Database;

namespace Cloudea.Domain.Forum.Entities.Recommend;

public class UserPostInterest : BaseDataEntity
{
    public Guid UserId { get; set; }

    public List<PostInterest> PostInterestList { get; set; } = [];

    public class PostInterest
    {
        public Guid PostId { get; set; }

        public double Score { get; set; }
    }
}
