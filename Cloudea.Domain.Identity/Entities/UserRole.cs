using Cloudea.Infrastructure.Database;

namespace Cloudea.Domain.Identity.Entities;

public class UserRole : BaseDataEntity
{
    public Guid UserId { get; set; }
    public int RoleId { get; set; }
}
