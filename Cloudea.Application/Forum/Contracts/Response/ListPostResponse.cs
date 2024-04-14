using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Forum.Entities;
using Cloudea.Domain.Identity.Entities;

namespace Cloudea.Application.Forum.Contracts.Response
{
    public class ListPostResponse : PageResponse<ListPostResponse.Post>
    {
        private ListPostResponse()
        {
            Rows = [];
        }

        public static ListPostResponse Create(PageResponse<ForumPost> postList)
        {
            var response = new ListPostResponse()
            {
                Total = postList.Total,
                Rows = []
            };
            foreach (var post in postList.Rows)
            {
                response.Rows.Add(new Post()
                {
                    Id = post.Id,
                    ParentSectionId = post.ParentSectionId,
                    OwnerUserId = post.OwnerUserId,
                    Title = post.Title,
                    Content = post.Content,
                    ClickCount = post.ClickCount,
                    LikeCount = post.LikeCount,
                    DislikeCount = post.DislikeCount,
                    LastClickTime = post.LastClickTime,
                    LastEditTime = post.LastEditTime,
                    CreatedOnUtc = post.CreatedOnUtc,
                });
            }

            return response;
        }

        public class Post
        {
            public Guid Id { get; set; }
            public Guid ParentSectionId { get; set; }
            public Guid OwnerUserId { get; set; }
            public UserProfile OwnerUser { get; set; } = default!;

            public string Title { get; set; } = string.Empty;
            public string Content { get; set; } = string.Empty;
            public long ClickCount { get; set; }

            public long LikeCount { get; set; }
            public long DislikeCount { get; set; }

            public DateTimeOffset LastClickTime { get; set; }
            public DateTimeOffset LastEditTime { get; set; }

            public DateTimeOffset CreatedOnUtc { get; set; }
        }
    }
}
