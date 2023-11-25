using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Infrastructure.Domain
{
    public interface IPrimaryKey
    {
        public long AutoIncId { get; }
    }
}
