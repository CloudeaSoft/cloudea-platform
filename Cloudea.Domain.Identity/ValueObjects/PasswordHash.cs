using Cloudea.Domain.Common.Primitives;
using Cloudea.Domain.Common.Shared;

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
