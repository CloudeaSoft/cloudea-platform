using Cloudea.Infrastructure.Database;
using FreeSql.DataAnnotations;

namespace Cloudea.Service.Auth.Domain.Entities;

[AutoGenerateTable]
[Table(Name = "auth_user_role")]
public class UserRole : BaseEntity
{
    public Guid UserId { get; set; }
    public int RoleId { get; set; }
}
