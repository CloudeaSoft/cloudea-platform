using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Infrastructure.Domain
{
    public interface IDomainEntity : IPrimaryKey
    {
        /// <summary>
        /// 逻辑用guid主键
        /// </summary>
        public Guid Id { get; }
    }
}
