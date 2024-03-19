using Cloudea.Domain.Forum.Entities.Recommend;
using Cloudea.Domain.Forum.Repositories.Recommend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Persistence.Repositories.Forum.Forum
{
    public class UserPostInterestRepository : IUserPostInterestRepository
    {
        private readonly ApplicationDbContext _context;

        public UserPostInterestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(UserPostInterest userPostInterest) =>
            _context.Add(userPostInterest);
    }
}
