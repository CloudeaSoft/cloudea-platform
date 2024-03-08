using Cloudea.Infrastructure.Database;
using Cloudea.Infrastructure.Primitives;
using Cloudea.Service.Forum.DomainEvents;

namespace Cloudea.Service.Forum.Domain.Entities
{
    /// <summary>
    /// 论坛回复帖
    /// </summary>
    public sealed class ForumReply : AggregateRoot, IAuditableEntity
    {
        private ForumReply(
            Guid id,
            Guid userId,
            Guid postId,
            string content) : base(id)
        {
            ParentPostId = postId;
            OwnerUserId = userId;
            Content = content;
            LikeCount = 0;
        }

        private ForumReply() { }

        public Guid ParentPostId { get; private set; }
        public Guid OwnerUserId { get; private set; }

        public string? Title { get; private set; } = null;
        public string Content { get; private set; }
        public long LikeCount { get; private set; }

        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        public static ForumReply? Create(Guid userId, ForumPost post, string content)
        {
            if (string.IsNullOrEmpty(content)) {
                return null;
            }

            var res = new ForumReply(Guid.NewGuid(), userId, post.Id, content);

            res.RaiseDomainEvent(new ReplyCreatedDomainEvent(
                Guid.NewGuid(),
                res.Id,
                res.ParentPostId));

            return res;
        }


        public void Update(string? content)
        {
            if (!string.IsNullOrEmpty(content)) {
                Content = content;
            }
        }
    }
}
