using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Base.Authentication
{
    public interface IPermissionService
    {
        Task<HashSet<string>> GetPermissionsAsync(long userId);
    }
}
