using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Domain.Identity.Enums
{
    public enum ReportStatus
    {
        /// <summary>  
        /// 举报未处理  
        /// </summary>  
        Pending,

        /// <summary>  
        /// 举报处理中  
        /// </summary>  
        InProgress,

        /// <summary>  
        /// 举报已解决  
        /// </summary>  
        Resolved,

        /// <summary>  
        /// 举报已忽略  
        /// </summary>  
        Ignored,

        /// <summary>  
        /// 举报无效  
        /// </summary>  
        Invalid
    }
}
