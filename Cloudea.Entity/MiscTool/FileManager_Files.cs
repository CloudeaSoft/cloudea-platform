using Cloudea.Infrastructure.Db;
using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Entity.MiscTool; 
public class FileManager_Files : EntityBase {

    /// <summary>
    /// 文件名称
    /// </summary>
    public string FileName { get; set; }
    /// <summary>
    /// 随机名称 GUID
    /// </summary>
    public string RandomName { get; set; }
    /// <summary>
    /// 文件地址
    /// </summary>
    public string FilePath { get; set; }
    /// <summary>
    /// 内容类型
    /// </summary>
    public string ContentType { get; set; }
    /// <summary>
    /// 上传人
    /// </summary>
    public long? CreateEmployeeId { get; set; }
}
