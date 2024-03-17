using FluentValidation;

namespace Cloudea.Infrastructure.Message;

public class SendEmailRequest_MailAddress_Validator : AbstractValidator<string>
{
    public SendEmailRequest_MailAddress_Validator()
    {
        RuleFor(x => x).Must(Test).WithMessage(x => $"收件人邮箱:{x}解析失败");
    }

    public static bool Test(string address)
    {
        return true;
    }
}
