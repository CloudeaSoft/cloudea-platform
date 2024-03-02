using Cloudea.Service.Auth.Domain.Entities;
using Cloudea.Service.Auth.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cloudea.Persistence.Repositories.Identity
{
    public class VerificationCodeRepository : IVerificationCodeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public VerificationCodeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(VerificationCode code)
        {
            _dbContext.Set<VerificationCode>().Add(code);
        }

        public void Update(VerificationCode code)
        {
            _dbContext.Set<VerificationCode>().Update(code);
        }

        public void Delete(VerificationCode code)
        {
            _dbContext.Set<VerificationCode>().RemoveRange(code);
        }

        public async Task<VerificationCode?> GetByEmailAndCodeTypeAsync(string email, VerificationCodeType codeType, CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .Set<VerificationCode>()
                .Where(t => t.Email == email && t.VerCodeType == codeType)
                .OrderBy(x => x.CreatedOnUtc)
                .FirstOrDefaultAsync();
        }
    }
}
