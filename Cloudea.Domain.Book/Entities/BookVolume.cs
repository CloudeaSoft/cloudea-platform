using Cloudea.Domain.Common.Database;

namespace Cloudea.Domain.Book.Entities;

public class BookVolume : BaseDataEntity, ISoftDelete
{
    /// <summary>
    /// 书号
    /// </summary>
    public Guid Meta_Id { get; set; }
    /// <summary>
    /// 章节名
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// 创建者
    /// </summary>
    public Guid Creator { get; set; }
    /// <summary>
    /// 语言
    /// </summary>
    public string? Language { get; set; }
    /// <summary>
    /// 译者
    /// </summary>
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

    public DateTimeOffset? DeletionTime { get; set; }

    public void SoftDelete()
    {
        IsDeleted = false;
        DeletionTime = DateTimeOffset.Now;
    }
}
