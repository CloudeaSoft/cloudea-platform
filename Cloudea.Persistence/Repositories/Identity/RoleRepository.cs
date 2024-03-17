using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Identity.Entities;
using Cloudea.Domain.Identity.Repositories;
using Cloudea.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Cloudea.Persistence.Repositories.Identity
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RoleRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Role role)
        {
            _dbContext.Add(role);
        }

        public void Update(Role role)
        {
            _dbContext.Update(role);
        }

        public async Task<Role?> GetByIdAsync(int roleId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Role>().Where(x => x.Value == roleId).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Role?> GetByNameAsync(string roleName, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Role>().Where(x => x.Name == roleName).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PageResponse<Role>> GetPageAsync(PageRequest request, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Role>().ToPageListAsync(request, cancellationToken);
        }
    }
}
