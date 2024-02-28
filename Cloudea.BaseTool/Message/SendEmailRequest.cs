using Cloudea.Infrastructure.Shared;
using MediatR;
using System.Net.Mail;

namespace Cloudea.Service.Base.Message;

/// <summary>
/// 发送邮件
/// </summary>
public class SendEmailRequest : IRequest<Result> {
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

    public Result Check() {
        var validator = new SendEmailRequest_Validator();
        var res = validator.Validate(this);
        if (res.IsValid is not true) {
            return Result.Failure(new Error("SendEmailRequest.InvalidParam", res.Errors.ToString()));
        }
        return Result.Success();
    }
}
