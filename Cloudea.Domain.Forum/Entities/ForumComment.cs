using Cloudea.Domain.Common.Primitives;

namespace Cloudea.Domain.Forum.Entities;

/// <summary>
/// 论坛回复帖评论
/// </summary>
public sealed class ForumComment : Entity, IAuditableEntity
{
    private ForumComment(
        Guid id,
        Guid parentReplyId,
        Guid ownerUserId,
        Guid? targetUserId,
        string content) : base(id)
    {
        ParentReplyId = parentReplyId;
        OwnerUserId = ownerUserId;
        TargetUserId = targetUserId;
        Content = content;
        LikeCount = 0;
    }

    private ForumComment() { }

    // 关系信息
    public Guid ParentReplyId { get; set; }
    public Guid OwnerUserId { get; set; }
    public Guid? TargetUserId { get; set; }
    // 内容信息
    public string Content { get; set; }
    public long LikeCount { get; set; }
    public long DislikeCount { get; set; }
    // 时间信息
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }

    internal static ForumComment? Create(ForumReply reply, Guid ownerUser, Guid? targetUser, string content)
    {
        if (reply is null || reply.Id == Guid.Empty) {
            return null;
        }
        if (ownerUser == Guid.Empty) {
            return null;
        }
        if (string.IsNullOrEmpty(content)) {
            return null;
        }

        var commentId = Guid.NewGuid();
        return new(commentId, reply.Id, ownerUser, targetUser, content);
    }


    public void Update(string content)
    {
        if (!string.IsNullOrEmpty(content)) {
            Content = content;
        }
    }
}