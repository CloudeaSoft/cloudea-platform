using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Forum.Entities;
using Cloudea.Domain.Identity.Entities;
using System.Linq;

namespace Cloudea.Application.Forum.Contracts;

public class ListCommitPageResponse : PageResponse<ListCommitPageResponse.Comment>
{
    private ListCommitPageResponse(long total, List<Comment> rows)
    {
        Rows = rows;
        Total = total;
    }

    public static ListCommitPageResponse Create(PageResponse<ForumComment> commentList, List<UserProfile> userProfileList)
    {
        return new(commentList.Total,
            commentList.Rows.Select(x => 
                Comment.Create(x, userProfileList.Where(y => y.Id.Equals(x.OwnerUserId)).FirstOrDefault()!)
                ).ToList());
    }



    public class Comment
    {
        public Comment(Guid commentId, Guid creatorId, UserProfile creator, string content, long likeCount, long dislikeCount, DateTimeOffset createTime)
        {
            CommentId = commentId;
            CreatorId = creatorId;
            Content = content;
            LikeCount = likeCount;
            DislikeCount = dislikeCount;
            CreateTime = createTime;
        }

        public Guid CommentId { get; set; }
        public UserProfile Creator { set; get; } = default!;
        public Guid CreatorId { set; get; }

        public string Content { get; set; }
        public long LikeCount { get; set; }
        public long DislikeCount { get; set; }

        public DateTimeOffset CreateTime { get; set; }

        public static Comment Create(ForumComment comment, UserProfile profile) =>
            new(
                comment.Id,
                comment.OwnerUserId,
                profile,
                comment.Content,
                comment.LikeCount,
                comment.DislikeCount,
                comment.CreatedOnUtc
            );
    }
}
