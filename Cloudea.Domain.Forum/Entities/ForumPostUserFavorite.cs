using Cloudea.Domain.Common.Primitives;

namespace Cloudea.Domain.Forum.Entities
{
    public class ForumPostUserFavorite : Entity, IAuditableEntity
    {
        public ForumPostUserFavorite(
            Guid id,
            Guid postId,
            Guid userId) : base(id)
        {
            ParentPostId = postId;
            OwnerUserId = userId;
        }

        public ForumPostUserFavorite() { }

        public Guid ParentPostId { get; set; }

        public Guid OwnerUserId { get; set; }

        public DateTimeOffset CreatedOnUtc { get; private set; }

        public DateTimeOffset? ModifiedOnUtc { get; private set; }
        public static ForumPostUserFavorite? Create(ForumPost post, Guid userId)
        {
            if (post is null || post.Id == Guid.Empty) return null;
            if (userId == Guid.Empty) return null;

            var dislikeId = Guid.NewGuid();

            return new ForumPostUserFavorite(dislikeId, post.Id, userId);
        }
    }
}
