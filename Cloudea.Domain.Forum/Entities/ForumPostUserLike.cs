using Cloudea.Domain.Common.Primitives;

namespace Cloudea.Domain.Forum.Entities
{
    public class ForumPostUserLike : Entity, IAuditableEntity
    {
        public ForumPostUserLike(
            Guid id,
            Guid postId,
            Guid userId,
            bool isLike) : base(id)
        {
            ParentPostId = postId;
            OwnerUserId = userId;
            IsLike = isLike;
        }

        public ForumPostUserLike() { }

        public Guid ParentPostId { get; set; }

        public Guid OwnerUserId { get; set; }

        public bool IsLike { get; set; }

        public DateTimeOffset CreatedOnUtc { get; private set; }
        public DateTimeOffset? ModifiedOnUtc { get; private set; }

        public static ForumPostUserLike? Create(ForumPost post, Guid userId, bool isLike = true)
        {
            if (post is null || post.Id == Guid.Empty) return null;
            if (userId == Guid.Empty) return null;

            var dislikeId = Guid.NewGuid();

            return new ForumPostUserLike(dislikeId, post.Id, userId, isLike);
        }
    }
}
