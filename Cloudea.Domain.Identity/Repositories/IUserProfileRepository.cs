using Cloudea.Domain.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Domain.Identity.Repositories
{
    public interface IUserProfileRepository
    {
        void Add(UserProfile user);

        void Update(UserProfile user);

        void Delete(UserProfile user);

        Task<UserProfile?> GetByUserIdAsync(Guid userId,CancellationToken cancellationToken = default);
    }
}
