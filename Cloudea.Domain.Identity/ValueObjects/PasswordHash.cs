using Cloudea.Domain.Common.Shared;
using Cloudea.Infrastructure.Primitives;
using Cloudea.Infrastructure.Shared;
using Cloudea.Infrastructure.Utils;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Domain.Identity.ValueObjects
{
    public class PasswordHash : ValueObject
    {
        public const int MinLength = 6;
        public const int MaxLength = 16;

        private PasswordHash(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static Result<PasswordHash> Create(string passwordHash)
        {
            return new PasswordHash(passwordHash);
        }
    }
}
