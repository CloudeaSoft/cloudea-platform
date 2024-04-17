using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Forum.Entities;
using Cloudea.Domain.Identity.Entities;

namespace Cloudea.Application.Forum.Contracts.Response
{
    public class ListPostResponse : PageResponse<PostInfo>
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
                response.Rows.Add(PostInfo.Create(post));
            }

            return response;
        }
    }
}
