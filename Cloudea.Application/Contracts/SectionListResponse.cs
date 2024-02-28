using Cloudea.Infrastructure.Shared;

namespace Cloudea.Service.Forum.Domain.Models
{
    public class SectionListResponse : PageResponse<SectionInfo>
    {

    }

    public class SectionInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
