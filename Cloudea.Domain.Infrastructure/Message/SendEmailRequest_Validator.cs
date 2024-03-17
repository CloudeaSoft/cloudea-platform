using Cloudea.Application.Utils;
using FluentValidation;

namespace Cloudea.Infrastructure.Message;

public class SendEmailRequest_Validator : AbstractValidator<SendEmailRequest>
{
    public SendEmailRequest_Validator()
    {
        RuleFor(x => x.To.Count).NotEqual(0).WithMessage("收件人不能为空");
        RuleForEach(x => x.To).SetValidator(new SendEmailRequest_MailAddress_Validator()).WithMessage("收件人邮箱:{CollectionIndex}解析失败");
        RuleForEach(x => x.Cc).SetValidator(new SendEmailRequest_MailAddress_Validator()).WithMessage("抄送人邮箱:{CollectionIndex}解析失败");
        RuleFor(x => x.Subject).NotNull().NotEmpty().WithMessage("主题不能为空");
        RuleFor(x => x.Body).NotNull().NotEmpty().WithMessage("主题不能为空");
    }
}
