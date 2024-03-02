using Cloudea.Service.Auth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Auth.Domain.Repositories
{
    public interface IVerificationCodeRepository
    {
        void Add(VerificationCode code);

        void Update(VerificationCode code);

        void Delete(VerificationCode code);

        Task<VerificationCode?> GetByEmailAndCodeTypeAsync(string email, VerificationCodeType codeType, CancellationToken cancellationToken = default);
    }
}
