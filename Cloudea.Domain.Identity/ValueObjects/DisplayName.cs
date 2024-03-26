using Cloudea.Domain.Common.Primitives;

namespace Cloudea.Domain.Identity.ValueObjects
{
    public class DisplayName : ValueObject
    {
        private DisplayName(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static DisplayName Create(string name)
        {
            return new DisplayName(name);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
