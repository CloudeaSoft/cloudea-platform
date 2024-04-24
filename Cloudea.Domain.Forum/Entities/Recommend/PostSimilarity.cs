using Cloudea.Domain.Common.Database;

namespace Cloudea.Domain.Forum.Entities.Recommend;

public class PostSimilarity : BaseDataEntity
{
    private PostSimilarity(Guid id, Guid postId, Guid relatedPostId, double score)
    {
        Id = id;
        PostId = postId;
        RelatedPostId = relatedPostId;
        Score = score;
    }

    public Guid PostId { get; set; }

    public Guid RelatedPostId { get; set; }

    public double Score { get; set; }

    public static PostSimilarity Create(Guid postId, Guid relatedPostId, double score)
    {
        return new(Guid.NewGuid(), postId, relatedPostId, score);
    }
}
