using Cloudea.Service.Base.Message;
using Microsoft.Extensions.Options;

namespace Cloudea.Web.OptionsSetup {
    public class MailOptionsSetup : IConfigureOptions<MailOptions>
    {
        private readonly IConfiguration _configuration;

        public MailOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(MailOptions options)
        {
            _configuration.GetSection(MailOptions.SectionName).Bind(options);
        }
    }
}
