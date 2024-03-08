using Cloudea.Domain.Identity.Entities;
using Cloudea.Infrastructure.Shared;

namespace Cloudea.Service.Auth.Domain.Repositories
{
    public interface IRoleRepository
    {
        void Add(Role role);

        void Update(Role role);

        Task<Role?> GetByIdAsync(int roleId, CancellationToken cancellationToken = default);

        Task<Role?> GetByNameAsync(string roleName, CancellationToken cancellationToken = default);

        Task<PageResponse<Role>> GetPageAsync(PageRequest request, CancellationToken cancellationToken = default);
    }
}
