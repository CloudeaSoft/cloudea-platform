using Cloudea.Infrastructure.Database;
using FreeSql.DataAnnotations;
using FreeSql.Internal.Model;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColumnAttribute = FreeSql.DataAnnotations.ColumnAttribute;

namespace Cloudea.Entity.Forum;

/// <summary>
/// 论坛主题帖
/// </summary>
[AutoGenerateTable]
public class Forum_Topic : BaseEntity
{
    /// <summary>
    /// 对应板块
    /// </summary>
    public long SectionId { get; set; }
    /// <summary>
    /// 用户ID - 发帖人
    /// </summary>
    public long UserId { get; set; }
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// 内容
    /// </summary>
    public string? Content { get; set; }
    /// <summary>
    /// 点击次数
    /// </summary>
    public int ClickCount {  get; set; }
    /// <summary>
    /// 最后点击时间时间
    /// </summary>
    [Column(DbType = "timestamp")]
    public DateTime LastClickTime { get; set; }
}