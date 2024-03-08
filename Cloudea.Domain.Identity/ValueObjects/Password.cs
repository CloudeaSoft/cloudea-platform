using Cloudea.Infrastructure.Primitives;
using Cloudea.Infrastructure.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Domain.Identity.ValueObjects
{
    public class Password : ValueObject
    {
        public Password(string value)
        {
            Value = value;
        }

        public string Value { get; set; }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static Result<Password> Create(string password)
        {
            return new Password(password);
        }
    }
}
