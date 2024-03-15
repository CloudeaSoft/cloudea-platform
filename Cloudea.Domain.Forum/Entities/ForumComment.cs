using Cloudea.Infrastructure.Database;
using Cloudea.Infrastructure.Primitives;

namespace Cloudea.Service.Forum.Domain.Entities;

/// <summary>
/// 论坛回复帖评论
/// </summary>
public sealed class ForumComment : BaseDataEntity, IAuditableEntity
{
    private ForumComment(
        Guid parentReplyId,
        Guid ownerUserId,
        Guid? targetUserId,
        string content)
    {
        Id = Guid.NewGuid();
        ParentReplyId = parentReplyId;
        OwnerUserId = ownerUserId;
        TargetUserId = targetUserId;
        Content = content;
        LikeCount = 0;
    }

    private ForumComment()
    {

    }

    // 关系信息
    public Guid ParentReplyId { get; set; }
    public Guid OwnerUserId { get; set; }
    public Guid? TargetUserId { get; set; }
    // 内容信息
    public string Content { get; set; }
    public long LikeCount { get; set; }
    public long DislikeCount {  get; set; }
    // 时间信息
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public static ForumComment? Create(ForumReply reply, Guid ownerUser, Guid? targetUser, string content)
    {
        if (string.IsNullOrEmpty(content)) {
            return null;
        }
        return new(reply.Id, ownerUser, targetUser, content);
    }


    public void Update(string content)
    {
        if (!string.IsNullOrEmpty(content)) {
            Content = content;
        }
    }
}