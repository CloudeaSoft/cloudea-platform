using Cloudea.Infrastructure.Database;
using System.ComponentModel.DataAnnotations;

namespace Cloudea.Entity.Base.File;
/// <summary>
/// 文件
/// </summary>
[AutoGenerateTable]
public class Base_File : BaseEntity
{
    /// <summary>
    /// 完整文件名
    /// </summary>
    [StringLength(550)]
    public string FullName { get; set; }
    /// <summary>
    /// 文件名
    /// </summary>
    [StringLength(500)]
    public string FileName { get; set; }
    /// <summary>
    /// 文件格式
    /// </summary>
    [StringLength(50)]
    public string Extension { get; set; }
    /// <summary>
    /// 随机名称 GUID
    /// </summary>
    [StringLength(50)]
    public string RandomName { get; set; }
    /// <summary>
    /// 缩略/压缩 路径
    /// </summary>
    [StringLength(1000)]
    public string ThumbnailPath { get; set; }
    /// <summary>
    /// 文件路径
    /// </summary>
    [StringLength(1000)]
    public string FilePath { get; set; }
    /// <summary>
    /// 内容类型
    /// </summary>
    [StringLength(100)]
    public string ContentType { get; set; }
}
