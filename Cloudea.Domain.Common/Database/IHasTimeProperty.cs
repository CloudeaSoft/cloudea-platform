using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Domain.Common.Database
{
    public interface IHasTimeProperty : IHasCreationTime, IHasModificationTime
    {
    }
}
