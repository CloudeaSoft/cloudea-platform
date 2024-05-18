using Cloudea.Domain.System.ValueObjects;

namespace Cloudea.Domain.System.Primitives;

public interface IInternaltional
{
    LanguageCode LanguageCode { get; init; }
}
