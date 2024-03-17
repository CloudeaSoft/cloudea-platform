using System.Text.Json.Serialization;

namespace Cloudea.Domain.Common.Shared;

/// <summary>
/// 返回结果
/// </summary>
public class Result
{
    protected internal Result(bool succeeded, Error error)
    {
        if (succeeded && error != Error.None ||
            !succeeded && error == Error.None) {
            throw new InvalidOperationException();
        }

        Status = succeeded;
        Error = error;
    }

    public bool Status { get; }

    public Error Error { get; }

    [JsonIgnore]
    public bool IsSuccess { get { return Status; } }

    [JsonIgnore]
    public bool IsFailure { get { return !Status; } }

    public static Result Success()
        => new(true, Error.None);

    public static Result<TValue> Success<TValue>(TValue value) => new(true, Error.None, value);

    public static Result Failure(Error error)
        => new(false, error);

    public static Result<TValue> Failure<TValue>(Error error) => new(false, error, default);

    public static implicit operator Result(Error error) => Failure(error);
}

/// <summary>
/// 返回结果
/// </summary>
/// <typeparam name="TValue"></typeparam>
public class Result<TValue> : Result
{
    private readonly TValue? _data;

    protected internal Result(bool status, Error error, TValue? value = default)
        : base(status, error) =>
        _data = value;

    public TValue Data => Status
        ? _data!
        : throw new InvalidOperationException("The value of a failure result can not be accessed");

    public static implicit operator Result<TValue>(TValue value) => Create(value);

    public static implicit operator Result<TValue>(Error error) => new(false, error);

    private static Result<TValue> Create(TValue? value)
    {
        return new(true, Error.None, value);
    }
}
