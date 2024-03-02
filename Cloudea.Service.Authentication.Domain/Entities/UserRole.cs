using Cloudea.Infrastructure.Database;

namespace Cloudea.Service.Auth.Domain.Entities;

public class UserRole : BaseDataEntity
{
    public Guid UserId { get; set; }
    public int RoleId { get; set; }
}
