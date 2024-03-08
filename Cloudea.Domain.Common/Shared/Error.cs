namespace Cloudea.Domain.Common.Shared;

/// <summary>
/// 错误
/// </summary>
/// <param name="Code"></param>
/// <param name="Message"></param>
public sealed class Error(string Code, string? Message = null) : IEquatable<Error>
{
    /// <summary>
    /// 无错误
    /// </summary>
    public static readonly Error None = new(string.Empty);
    /// <summary>
    /// Null值错误
    /// </summary>
    public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null");

    public string Code { get; } = Code;
    public string? Message { get; } = Message;

    public static implicit operator string(Error error) => error.Code;

    public static bool operator ==(Error? a, Error? b)
    {
        if (a is null && b is null) return true;

        if (a is null || b is null) return false;

        return a.Code == b.Code;
    }

    public static bool operator !=(Error? a, Error? b)
    {
        return !(a == b);
    }

    public bool Equals(Error? other)
    {
        if (other is null) return false;

        if (other.GetType() != GetType()) return false;

        return other.Code == Code;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;

        if (obj.GetType() != GetType()) return false;

        if (obj is not Error error) {
            return false;
        }

        return error.Code == Code;
    }

    public override int GetHashCode()
    {
        return Code.GetHashCode();
    }
}