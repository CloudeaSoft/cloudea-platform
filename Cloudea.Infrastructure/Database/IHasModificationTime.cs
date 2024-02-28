using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Infrastructure.Database
{
    public interface IHasModificationTime
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime ModificationTime { get; }
    }
}
