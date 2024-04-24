using Cloudea.Domain.Common.Primitives;

namespace Cloudea.Domain.Forum.Entities.Recommend;

public class UserPostInterest : Entity
{
    private UserPostInterest(Guid id, Guid userId, Guid postId, double score)
        : base(id)
    {
        UserId = userId;
        PostId = postId;
        Score = score;
    }

    public Guid UserId { get; set; }

    public Guid PostId { get; set; }

    public double Score { get; set; }

    public static UserPostInterest Create(Guid userId, Guid postId, double score)
    {
        return new(Guid.NewGuid(), userId, postId, score);
    }
}
