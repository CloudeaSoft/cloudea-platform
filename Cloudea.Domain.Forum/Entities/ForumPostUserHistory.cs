using Cloudea.Domain.Common.Primitives;

namespace Cloudea.Domain.Forum.Entities
{
    public class ForumPostUserHistory : Entity, IAuditableEntity
    {
        private ForumPostUserHistory(Guid id, Guid userId, Guid postId) : base(id)
        {
            UserId = userId;
            PostId = postId;
        }

        private ForumPostUserHistory() { }

        public Guid UserId { get; set; }

        public Guid PostId { get; set; }

        public DateTimeOffset CreatedOnUtc { get; set; }

        public DateTimeOffset? ModifiedOnUtc { get; set; }

        internal static ForumPostUserHistory? Create(ForumPost post, Guid userId)
        {
            if (userId == Guid.Empty) {
                return null;
            }

            var historyId = Guid.NewGuid();

            return new ForumPostUserHistory(historyId, userId, post.Id);
        }
    }
}
