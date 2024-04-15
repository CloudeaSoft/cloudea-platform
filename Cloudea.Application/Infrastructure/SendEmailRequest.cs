using Cloudea.Domain.Common.Shared;
using MediatR;
using System.Net.Mail;

namespace Cloudea.Application.Utils;

/// <summary>
/// 发送邮件
/// </summary>
public class SendEmailRequest : IRequest<Result>
{
    /// <summary>
    /// 接收人 列表
    /// </summary>
    public List<string> To { get; set; } = [];

    /// <summary>
    /// 抄送人 列表
    /// </summary>
    public List<string> Cc { get; set; } = [];

    /// <summary>
    /// 标题
    /// </summary>
    public required string Subject { get; set; }
    /// <summary>
    /// 内容
    /// </summary>
    public required string Body { get; set; }
    /// <summary>
    /// 内容是否为HTML
    /// </summary>
    public bool IsBodyHTML { get; set; }

    /// <summary>
    /// 附件
    /// </summary>
    public List<Attachment> Attachments { get; set; } = new List<Attachment>();
}
