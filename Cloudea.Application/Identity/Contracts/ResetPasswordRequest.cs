using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Application.Identity.Contracts
{
    public sealed record ResetPasswordRequest(string OldPassword, string NewPassword,string VerCode);
}
