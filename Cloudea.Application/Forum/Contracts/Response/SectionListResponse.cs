using Cloudea.Domain.Common.Shared;

namespace Cloudea.Application.Forum.Contracts.Response
{
    public class SectionListResponse : PageResponse<SectionInfo>
    {

    }

    public class SectionInfo
    {
        private SectionInfo(Guid id, string name, Guid masterUserId, string masterUserName)
        {
            Id = id;
            Name = name;
            UserId = masterUserId;
            UserName = masterUserName;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }

        public static SectionInfo Create(Guid id, string name, Guid masterUserId, string masterUserName)
        {
            return new SectionInfo(id, name, masterUserId, masterUserName);
        }
    }
}
