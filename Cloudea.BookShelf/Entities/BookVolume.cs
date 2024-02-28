using Cloudea.Infrastructure.Database;
using FreeSql.DataAnnotations;

namespace Cloudea.Service.Book.Domain.Entities;

[AutoGenerateTable]
[Table(Name = "book_volume")]
public class BookVolume : BaseDataEntity, ISoftDelete
{
    /// <summary>
    /// 书号
    /// </summary>
    public Guid Meta_Id { get; set; }
    /// <summary>
    /// 章节名
    /// </summary>
    [Column(DbType = "varchar(256)")]
    public string Title { get; set; }
    /// <summary>
    /// 创建者
    /// </summary>
    public Guid Creator { get; set; }
    /// <summary>
    /// 语言
    /// </summary>
    public string? Language {  get; set; }
    /// <summary>
    /// 译者
    /// </summary>
    [Column(DbType = "varchar(16)")]
    public string? Translator { get; set; }
    /// <summary>
    /// 章节内容
    /// </summary>
    public Guid Content_Id { get; set; }
    /// <summary>
    /// 字数
    /// </summary>
    public int Words { get; set; }

    public bool IsDeleted { get; private set; }

    [Column(DbType = "timestamp")]
    public DateTime? DeletionTime { get; set; }

    public void SoftDelete()
    {
        IsDeleted = false;
        DeletionTime = DateTime.Now;
    }
}
