using Cloudea.Domain.Common.Primitives;
using Cloudea.Domain.Common.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Domain.Identity.ValueObjects
{
    public class Salt : ValueObject
    {
        private Salt(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static Result<Salt> Create(string salt)
        {
            return new Salt(salt);
        }
    }
}
