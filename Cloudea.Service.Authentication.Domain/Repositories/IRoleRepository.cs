﻿using Cloudea.Infrastructure.Shared;
using Cloudea.Service.Auth.Domain.Entities;

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
