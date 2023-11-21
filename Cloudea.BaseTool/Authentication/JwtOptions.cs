using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Base.Authentication;

public class JwtOptions
{
    public const string SectionName = "Jwt";
    /// <summary>
    /// 密钥
    /// </summary>
    public string Secret { get; init; }
    /// <summary>
    /// 发行人
    /// </summary>
    public string Issuer { get; init; }
    /// <summary>
    /// 接受人
    /// </summary>
    public string Audience { get; init; }
}
