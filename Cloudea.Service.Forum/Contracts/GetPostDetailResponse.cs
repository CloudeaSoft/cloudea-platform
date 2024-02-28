using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Forum.Domain.Contracts;

public class GetPostDetailResponse
{
    public Guid Id { get; set; }

    public Guid OwnerUserId { get; set; }

    public string Title { get; set; }

    public DateTime LastEditTime { get; set; }

    public long ClickCount { get; set; }

    public List<ReplyDetail> Replys { get; set; }

    public class ReplyDetail
    {
        public Guid Id { get; set; }

        public Guid OwnerUserId { get; set; }

        public string Content { get; set; }

        public long LikeCount { get; set; }

        public List<CommentDetail> Comments { get; set; }

        public class CommentDetail
        {
            public Guid Id { get; set; }

            public Guid OwnerUserId { get; set; }

            public Guid TargetUserId { get; set; }

            public string Content { get; set; }

            public long LikeCount { get; set; }
        }
    }
}
