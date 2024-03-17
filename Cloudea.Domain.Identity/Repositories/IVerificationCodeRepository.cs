using Cloudea.Domain.Identity.Entities;
using Cloudea.Domain.Identity.Enums;

namespace Cloudea.Domain.Identity.Repositories
{
    public interface IVerificationCodeRepository
    {
        void Add(VerificationCode code);

        void Update(VerificationCode code);

        void Delete(VerificationCode code);

        Task<VerificationCode?> GetByEmailAndCodeTypeAsync(string email, VerificationCodeType codeType, CancellationToken cancellationToken = default);
    }
}
