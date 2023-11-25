using FreeSql.Internal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Infrastructure.Database
{
    public class Uri_String : TypeHandler<Uri>
    {
        public override Uri Deserialize(object value)
        {
            return new Uri((string)value);
        }

        public override object Serialize(Uri value)
        {
            return value.ToString();
        }
    }
}
