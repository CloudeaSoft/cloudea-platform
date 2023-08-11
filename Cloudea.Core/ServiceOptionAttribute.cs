using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Core
{
    public enum Lifetime
    {
        Singleton,
        Scoped,
        Transient
    }
    public class ServiceOptionAttribute:Attribute
    {
        public Lifetime Lifetime { get; set; }
        public ServiceOptionAttribute(Lifetime lifetime = Lifetime.Transient)
        {
            this.Lifetime = lifetime;
        }
    }
}
