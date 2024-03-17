using Cloudea.Domain.Common.Primitives;

namespace Cloudea.Domain.Forum.Entities
{
    public class ForumPostUserDislike : Entity
    {
        public ForumPostUserDislike(
            Guid id,
            Guid postId,
            Guid userId) : base(id)
        {
            ParentPostId = postId;
            OwnerUserId = userId;
        }

        public ForumPostUserDislike() { }

        public Guid ParentPostId { get; set; }

        public Guid OwnerUserId { get; set; }

        public static ForumPostUserDislike? Create(ForumPost post, Guid userId)
        {
            if (post is null || post.Id == Guid.Empty) return null;
            if (userId == Guid.Empty) return null;

            var dislikeId = Guid.NewGuid();

            return new ForumPostUserDislike(dislikeId, post.Id, userId);
        }
    }
}
