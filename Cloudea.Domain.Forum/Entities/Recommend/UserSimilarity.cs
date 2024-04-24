using Cloudea.Domain.Common.Database;

namespace Cloudea.Domain.Forum.Entities.Recommend;

public class UserSimilarity : BaseDataEntity
{
    private UserSimilarity(Guid id, Guid userId, Guid relatedUserId, double score)
    {
        Id = id;
        UserId = userId;
        RelatedUserId = relatedUserId;
        Score = score;
    }

    public Guid UserId { get; set; }

    public Guid RelatedUserId { get; set; }

    public double Score { get; set; }

    public static UserSimilarity Create(Guid userId, Guid relatedUserId, double score)
    {
        return new(Guid.NewGuid(), userId, relatedUserId, score);
    }
}
