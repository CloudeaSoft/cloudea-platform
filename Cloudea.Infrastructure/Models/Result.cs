using System;

namespace Cloudea.Infrastructure.Models
{
    /// <summary>
    /// 返回结果
    /// </summary>
    public class Result
    {
        public Result(bool succeeded, object data = null)
        {
            Status = succeeded;
            this.Data = data;
        }

        public bool Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public static Result Success(object data = null, string message = null)
        {
            var result = new Result(true, data);
            result.Message = message ?? "成功";
            return result;
        }

        public static Result Fail(string error = "", object data = null)
        {
            var res = new Result(false);
            res.Message = error ?? "失败";
            res.Data = data;
            return res;
        }

    }

    /// <summary>
    /// 返回结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T>
    {
        public Result(bool succeeded, T data = default(T))
        {
            Status = succeeded;
            this.Data = data;
        }

        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static Result<T> Success(T data = default(T), string message = null)
        {
            var result = new Result<T>(true, data);
            result.Message = message ?? "成功";
            return result;
        }

        public static Result<T> Fail(string error = "", T data = default(T))
        {
            var res = new Result<T>(false);
            res.Message = error ?? "失败";
            res.Data = data;
            return res;
        }

        public static implicit operator Result<T>(Result response)
        {
            if (!(response.Data is T) && response.Data != null)
            {
                throw new ArgumentException($"Data的类型与{typeof(T).Name}不符");
            }
            if (response.Data != null)
            {
                return new Result<T>(response.Status)
                {
                    Data = (T)response.Data,
                    Message = response.Message
                };
            }
            else
            {
                return new Result<T>(response.Status)
                {
                    Message = response.Message
                };
            }
        }
    }
