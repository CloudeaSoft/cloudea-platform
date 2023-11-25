using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Infrastructure.Domain
{
    public interface IHasCreationTime
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreationTime { get; }
    }
}
