using Cloudea.Infrastructure.Database;
using Cloudea.Infrastructure.Domain;

namespace Cloudea.Service.Auth.Domain.Entities;

[AutoGenerateTable]
public record Auth_UserRole : BaseDomainEntity, IHasTimeProperty
{
    public DateTime CreationTime { get; private set; }
    public DateTime ModificationTime { get; private set; }

    public long UserId { get; set; }
    public int RoleId { get; set; }
}
