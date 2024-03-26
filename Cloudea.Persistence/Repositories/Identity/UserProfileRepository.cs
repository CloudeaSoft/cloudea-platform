using Cloudea.Domain.Identity.Entities;
using Cloudea.Domain.Identity.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Persistence.Repositories.Identity
{
    internal class UserProfileRepository : IUserProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public UserProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(UserProfile user) => 
            _context.Set<UserProfile>().Add(user);

        public void Delete(UserProfile user) => 
            _context.Set<UserProfile>().Remove(user);

        public void Update(UserProfile user) => 
            _context.Set<UserProfile>().Update(user);

        public async Task<UserProfile?> GetByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default) =>
            await _context.Set<UserProfile>()
                .Where(x => x.Id == userId)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}
