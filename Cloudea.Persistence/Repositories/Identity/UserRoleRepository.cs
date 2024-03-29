﻿using Cloudea.Domain.Identity.Entities;
using Cloudea.Domain.Identity.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cloudea.Persistence.Repositories.Identity
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly ApplicationDbContext _database;

        public UserRoleRepository(ApplicationDbContext database)
        {
            _database = database;
        }

        public async Task<List<int>> GetRoleListByUserId(Guid userId)
        {
            return await _database
                .Set<UserRole>()
                .Where(x => x.UserId == userId)
                .Select(x => x.RoleId)
                .ToListAsync();
        }
    }
}
