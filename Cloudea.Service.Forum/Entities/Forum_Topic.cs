using Cloudea.Infrastructure.Database;
using FreeSql.DataAnnotations;

namespace Cloudea.Entity.Forum;
/// <summary>
/// 论坛主题帖实体
/// </summary>
[AutoGenerateTable]
[Index("uk_section_topic", "SectionId,Id", true)]
public record Forum_Topic : BaseDataEntity
{
    /// <summary>
    /// 对应板块
    /// </summary>
    public Guid SectionId { get; set; }
    /// <summary>
    /// 用户ID - 发帖人
    /// </summary>
    public Guid UserId { get; set; }
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; }
    /// <summary>
    /// 点击次数
    /// </summary>
    public int ClickCount { get; set; }
    /// <summary>
    /// 最后点击时间时间
    /// </summary>
    [Column(DbType = "timestamp")]
    public DateTime LastClickTime { get; set; }

    /// <summary>
    /// 创建对象
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="sectionId"></param>
    /// <param name="title"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    public static Forum_Topic Create(
        Guid userId,
        Guid sectionId,
        string title,
        string content)
    {
        return new Forum_Topic() {
            Id = Guid.NewGuid(),
            SectionId = sectionId,
            UserId = userId,
            Title = title,
            Content = content,
            ClickCount = 0,
            LastClickTime = DateTime.Now
        };
    }
}
