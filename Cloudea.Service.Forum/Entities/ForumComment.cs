using Cloudea.Infrastructure.Database;
using Cloudea.Infrastructure.Primitives;
using Microsoft.IdentityModel.Tokens;

namespace Cloudea.Service.Forum.Domain.Entities;

/// <summary>
/// 论坛回复帖评论
/// </summary>
public sealed class ForumComment : BaseDataEntity, IAuditableEntity
{
    private ForumComment(
        Guid parentReplyId,
        Guid ownerUserId,
        Guid targetUserId,
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

    public Guid ParentReplyId { get; set; }
    public Guid OwnerUserId { get; set; }
    public Guid? TargetUserId { get; set; }

    public string Content { get; set; }
    public long LikeCount { get; set; }

    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public static ForumComment? Create(Guid ownerUser, ForumReply reply, Guid targetUser, string content)
    {
        if (string.IsNullOrEmpty(content)) {
            return null;
        }
        return new(ownerUser, reply.Id, targetUser, content);
    }


    public void Update(string content)
    {
        if (!string.IsNullOrEmpty(content)) {
            Content = content;
        }
    }
}