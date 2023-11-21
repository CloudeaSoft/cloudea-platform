using System;
using System.Net.Http.Headers;

namespace Cloudea.Infrastructure.Models;

public class ResultBase
{
    public bool Status { get; set; }
    public string Message { get; set; } = default!;

    public bool IsSuccess()
    {
        return Status;
    }

    public bool IsFailure()
    {
        return !Status;
    }
}

/// <summary>
/// 返回结果
/// </summary>
public class Result : ResultBase
{
    public object? Data { get; set; }

    public Result(bool succeeded, object? data = null)
    {
        Status = succeeded;
        Data = data;
    }

    public static Result Success(object? data = null, string? message = null)
    {
        var res = new Result(true, data);
        res.Message = message ?? "成功";
        return res;
    }

    public static Result Fail(string? error = null, object? data = null)
    {
        var res = new Result(false, data);
        res.Message = error ?? "失败";
        return res;
    }
}

/// <summary>
/// 返回结果
/// </summary>
/// <typeparam name="T"></typeparam>
public class Result<T> : ResultBase
{
    public T Data { get; set; }

    public Result(bool succeeded, T data = default)
    {
        Status = succeeded;
        Data = data;
    }

    public static Result<T> Success(T data, string? message = null)
    {
        var res = new Result<T>(true, data);
        res.Message = message ?? "成功";
        return res;
    }

    public static Result<T> Fail(string? error = null, T data = default)
    {
        var res = new Result<T>(false, data);
        res.Message = error ?? "失败";
        return res;
    }

    /// <summary>
    /// 隐式转换为泛型Result
    /// </summary>
    /// <param name="response"></param>
    public static implicit operator Result<T>(Result response)
    {
        if (response.Data is not T && response.Data != null) // data为不为空 且 data不符合要求
        {
            throw new ArgumentException($"Data的类型与{typeof(T).Name}不符");
        }
        else if (response.Data != null) // data为不为空 且 data符合要求
        {
            return new Result<T>(response.Status) {
                Data = (T)response.Data,
                Message = response.Message
            };
        }
        else // data为空
        {
            return new Result<T>(response.Status) {
                Message = response.Message
            };
        }
    }
}