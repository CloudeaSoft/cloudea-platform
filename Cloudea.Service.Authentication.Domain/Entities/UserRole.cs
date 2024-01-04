using Cloudea.Infrastructure.Database;
using Cloudea.Infrastructure.Domain;
using FreeSql.DataAnnotations;

namespace Cloudea.Service.Auth.Domain.Entities;

[AutoGenerateTable]
[Table(Name = "auth_user_role")]
public record UserRole : BaseDomainEntity
{
    public Guid UserId { get; set; }
    public int RoleId { get; set; }
}
