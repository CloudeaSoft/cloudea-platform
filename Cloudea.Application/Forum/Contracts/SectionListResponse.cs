using Cloudea.Infrastructure.Shared;

namespace Cloudea.Application.Forum.Contracts
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
