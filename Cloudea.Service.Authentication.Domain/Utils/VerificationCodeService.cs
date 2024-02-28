using Cloudea.Infrastructure.Shared;
using Cloudea.Service.Auth.Domain.Entities;
using Cloudea.Service.Base.Message;
using MediatR;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cloudea.Service.Auth.Domain.Utils
{
    public class VerificationCodeService
    {
        private readonly ISender _sender;
        private readonly IFreeSql Database;

        public VerificationCodeService(ISender sender, IFreeSql database)
        {
            _sender = sender;
            Database = database;
        }

        const int EXPIRE_TIME = 5;

        /// <summary>
        /// 验证邮箱格式是否正确
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool CheckEmail(string email)
        {
            var re = @"/^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/";//正则表达式
            if (Regex.IsMatch(email, re))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 检查验证码
        /// </summary>
        /// <param name="email"></param>
        /// <param name="codeType"></param>
        /// <param name="code"></param>
        /// <param name="delete"></param>
        /// <returns></returns>
        public async Task<Result> CheckVerCodeEmail(string email, VerificationCodeType codeType, string code, bool delete = false)
        {
            /*if (CheckEmail(email) is false) {
                return Result.Fail("邮箱格式错误");
            }*/

            var entity = await Database.Select<UserVercode>()
                .Where(t => t.Email == email && t.VerCodeType == codeType).FirstAsync();
            if (entity == null)
            {
                return Result.Failure(new Error("验证码错误"));
            }

            if (entity.VerCode == code)
            {
                if (entity.VerCodeVaildTime.HasValue && entity.VerCodeVaildTime >= DateTime.Now)
                {
                    if (delete)
                    {
                        _ = Database.Delete<UserVercode>().Where(t => t.Id == entity.Id).ExecuteAffrowsAsync();
                    }

                    return Result.Success();
                }
                else
                {
                    return Result.Failure(new Error("验证码已过期请重新获取"));
                }
            }
            else
            {
                return Result.Failure(new Error("验证码错误"));
            }
        }

        /// <summary>
        /// 发送邮箱验证码
        /// </summary>
        /// <returns></returns>
        public async Task<Result> SendVerCodeEmail(string email, VerificationCodeType codeType)
        {
            // 生成验证码 并记录
            string verCode = GenerateVerificationCode();
            var entity = await Database.Select<UserVercode>()
                .Where(t => t.Email == email && t.VerCodeType == codeType).FirstAsync();
            if (entity == null)
            {
                entity = new UserVercode()
                {
                    Email = email,
                    VerCode = verCode,
                    VerCodeType = codeType,
                    VerCodeVaildTime = DateTime.Now.AddMinutes(EXPIRE_TIME)
                };
                await Database.Insert(entity).ExecuteAffrowsAsync();
            }
            else
            {
                // 如果时间还没超过一分钟又要发了  退回
                if (entity.VerCodeVaildTime.HasValue && entity.VerCodeVaildTime.Value.AddMinutes(-1 * (EXPIRE_TIME - 1)) >= DateTime.Now)
                {
                    return Result.Failure(new Error("请稍后再试"));
                }

                await Database.Update<UserVercode>()
                    .Set(t => t.VerCode, verCode)
                    .Set(t => t.VerCodeType, codeType)
                    .Set(t => t.VerCodeVaildTime, DateTime.Now.AddMinutes(EXPIRE_TIME))
                    .Where(t => t.Id == entity.Id)
                    .ExecuteAffrowsAsync();
            }

            // 生成邮件
            var codeEmail = new SendEmailRequest()
            {
                To = new() { email },
                Subject = "验证码",
                Body = $"[验证码] 验证码:{verCode}"
            };

            // 发送邮件
            var res = await _sender.Send(codeEmail);
            if (res.Status is false)
            {
                return res;
            }

            return Result.Success();
        }

        /// <summary>
        /// 随机生成验证码
        /// </summary>
        /// <returns></returns>
        private static string GenerateVerificationCode()
        {
            Random rd = new Random();
            return rd.Next(100000, 999999).ToString();
        }
    }
}
