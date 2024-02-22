using System.Runtime.CompilerServices;

namespace Cloudea.Service.Forum.Domain.Models {
    public class UpdateSectionRequest {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid MasterId { get; set; }
        public string? Statement { get; set; }
    }

    public static class UpdateSectionRequestExtension {
        public static List<Guid> GetIdList(this List<UpdateSectionRequest> request)
            => (from item in request select item.Id).ToList();
    }
}
