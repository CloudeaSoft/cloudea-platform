using Cloudea.Domain.Common.Primitives;
using Cloudea.Domain.Common.Shared;

namespace Cloudea.Domain.System.ValueObjects;

public class LanguageCode : ValueObject
{
    private LanguageCode(string language, string region)
    {
        Language = language;
        Region = region;
    }

    public string Language { get; }
    public string Region { get; }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return GetFullLocaleString();
    }

    public static Result<LanguageCode> Create(string language, string region)
    {
        if (string.IsNullOrWhiteSpace(language))
            return new Error("LanguageCode.InvalidParam", "Language cannot be null or whitespace.");
        if (string.IsNullOrWhiteSpace(region))
            return new Error("LanguageCode.InvalidParam", "Region cannot be null or whitespace.");

        return new LanguageCode(language, region);
    }

    public string GetFullLocaleString()
    {
        return $"{Language}-{Region}";
    }

    public override string ToString()
    {
        return GetFullLocaleString();
    }
}
