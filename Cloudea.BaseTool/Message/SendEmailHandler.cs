using MimeKit;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Cloudea.Infrastructure.Shared;

namespace Cloudea.Service.Base.Message;

public class SendEmailHandler : IRequestHandler<SendEmailRequest, Result>
{
    private readonly ILogger<SendEmailHandler> _logger;
    private readonly MailOptions _options;

    public SendEmailHandler(ILogger<SendEmailHandler> logger, IOptions<MailOptions> options)
    {
        _logger = logger;
        _options = options.Value;
    }

    public async Task<Result> Handle(SendEmailRequest request, CancellationToken cancellationToken)
    {

        await Task.Yield();
        var check = request.Check();
        if(check.Status is false) {
            return check;
        }

        try {

            // 发送邮件
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_options.DisplayName, _options.Username));

            foreach (var to in request.To) {
                message.To.Add(MailboxAddress.Parse(to));
            }

            foreach (var cc in request.Cc) {
                message.Cc.Add(MailboxAddress.Parse(cc));
            }

            message.Subject = request.Subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = request.Body;
            bodyBuilder.TextBody = request.Body;

            if (request.Attachments != null && request.Attachments.Count > 0) {
                foreach (var attchment in request.Attachments) {
                    bodyBuilder.Attachments.Add(attchment.Name, attchment.ContentStream);
                }
            }
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new MailKit.Net.Smtp.SmtpClient()) {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                //smtp服务器，端口，是否开启ssl
                client.Connect(_options.SMTP, _options.Port, _options.SSL);
                client.Authenticate(_options.Username, _options.Password);
                client.Send(message);
                client.Disconnect(true);
                client.Dispose();
            }
            return Result.Success();
        }
        catch (Exception ex) {
            _logger.LogInformation(ex, ex.ToString());
            return Result.Failure(new Error(ex.Message));
        }
        finally {
            foreach (var attch in request.Attachments) {
                attch.Dispose();
            }
        }
    }
}

