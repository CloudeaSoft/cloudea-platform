using Cloudea.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Auth.Domain.Abstractions
{
    public interface IUserRoleRepository
    {
        Task<Result<List<int>>> ReadByUserId(Guid userId);
    }
}
