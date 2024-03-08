using Cloudea.Domain.Identity.Entities;
using Cloudea.Service.Auth.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cloudea.Persistence.Repositories.Identity
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext database)
        {
            _dbContext = database;
        }

        public async Task<User?> GetByEmailAsync(string Email, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<User>().Where(t => t.Email == Email).FirstOrDefaultAsync();
        }

        public async Task<User?> GetByUserNameAsync(string UserName, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<User>().Where(t => t.UserName == UserName).FirstOrDefaultAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<User>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public void Add(User user)
        {
            _dbContext.Set<User>().Add(user);
        }

        public void Update(User user)
        {
            _dbContext.Set<User>().Update(user);
        }
    }
}
