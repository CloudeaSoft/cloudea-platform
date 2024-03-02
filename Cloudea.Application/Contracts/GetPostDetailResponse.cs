using Cloudea.Service.Forum.Domain.Entities;

namespace Cloudea.Application.Contracts;

public class GetPostDetailResponse
{
    public ForumPost Post { get; set; }

    public List<ReplyDetail> Replys { get; set; } = [];

    public class ReplyDetail
    {
        public ForumReply Reply { get; set; }

        public List<ForumComment> Comments { get; set; } = [];

        public static ReplyDetail Create(ForumReply reply)
        {
            return new ReplyDetail() {
                Reply = reply
            };
        }
    }
}
