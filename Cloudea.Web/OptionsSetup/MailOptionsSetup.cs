using Cloudea.Service.Base.Message;
using Microsoft.Extensions.Options;

namespace Cloudea.Web.OptionsSetup {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public class MailOptionsSetup : IConfigureOptions<MailOptions>
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    {
        private readonly IConfiguration _configuration;

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public MailOptionsSetup(IConfiguration configuration)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
            _configuration = configuration;
        }

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public void Configure(MailOptions options)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
            _configuration.GetSection(MailOptions.SectionName).Bind(options);
        }
    }
}
