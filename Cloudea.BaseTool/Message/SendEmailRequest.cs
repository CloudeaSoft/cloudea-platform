using MailKit;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Cloudea.Infrastructure.Models;
using System.Text.RegularExpressions;

namespace Cloudea.Service.Base.Message;

/// <summary>
/// 发送邮件
/// </summary>
public class SendEmailRequest : IRequest<Result>
{
    /// <summary>
    /// 接收人 列表
    /// </summary>
    public List<string> To { get; set; } = new List<string>();

    /// <summary>
    /// 抄送人 列表
    /// </summary>
    public List<string> Cc { get; set; } = new List<string>();

    /// <summary>
    /// 标题
    /// </summary>
    public string Subject { get; set; }
    /// <summary>
    /// 内容
    /// </summary>
    public string Body { get; set; }
    /// <summary>
    /// 内容是否为HTML
    /// </summary>
    public bool IsBodyHTML { get; set; }

    /// <summary>
    /// 附件
    /// </summary>
    public List<Attachment> Attachments { get; set; } = new List<Attachment>();

    public Result Check()
    {
        if (this.To.Count == 0) {
            return Result.Fail("收件人不能为空");
        }

        foreach (var to in this.To) {
            try {
                new MailAddress(to);
            }
            catch {
                return Result.Fail($"收件人邮箱:{to}解析失败");
            }
        }
        foreach (var cc in this.Cc) {
            try {
                new MailAddress(cc);
            }
            catch {
                return Result.Fail($"抄送人邮件:{cc}解析失败");
            }
        }

        if (string.IsNullOrEmpty(this.Subject)) {
            return Result.Fail("主题不能为空");
        }
        if (string.IsNullOrEmpty(this.Body)) {
            return Result.Fail("内容不能为空");
        }
        return Result.Success();
    }
}

