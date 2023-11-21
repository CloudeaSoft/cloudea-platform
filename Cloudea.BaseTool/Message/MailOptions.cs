using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Base.Message;
/// <summary>
/// 邮箱设置
/// </summary>
public class MailOptions
{
    public const string SectionName = "Message:Mail";
    /*
     * --- Example ---
      "DisplayName": "发报文",
      "Username": "lithelp@163.com",
      "Password": "lithelp123456",
      "SMTP": "smtp.163.com",
      "Port": 465
     */
    public string DisplayName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string SMTP { get; set; }
    public int Port { get; set; }
    public bool SSL { get; set; }

}
