using Cloudea.Domain.Common.Database;
using Cloudea.Domain.Common.Primitives;

namespace Cloudea.Domain.Book.Entities;

public sealed class BookMeta : BaseDataEntity, IAuditableEntity, ISoftDelete
{
    private BookMeta() { }

    /// <summary>
    /// 书名
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// 作者
    /// </summary>
    public string Author { get; set; }
    /// <summary>
    /// 简介
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// 创建者
    /// </summary>
    public Guid Creator { get; set; }
    /// <summary>
    /// 源语言 书名
    /// </summary>
    public string? Source_Title { get; set; }
    /// <summary>
    /// 源语言
    /// </summary>
    public string? Source_Language { get; set; }
    /// <summary>
    /// 源链接
    /// </summary>
    public string? Source_Link { get; set; }

    public bool IsDeleted { get; private set; }

    public DateTimeOffset? DeletionTime { get; private set; }

    public DateTimeOffset CreatedOnUtc { get; private set; }

    public DateTimeOffset? ModifiedOnUtc { get; private set; }

    /// <summary>
    /// 简单创建
    /// </summary>
    /// <param name="title"></param>
    /// <param name="author"></param>
    /// <param name="creator"></param>
    /// <returns></returns>
    public static BookMeta Create(
        Guid id,
        string title,
        string author,
        Guid creator)
    {
        BookMeta bookMeta = new() {
            Id = id,
            Title = title,
            Author = author,
            Creator = creator
        };
        return bookMeta;
    }

    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="id"></param>
    /// <param name="title"></param>
    /// <param name="author"></param>
    /// <param name="creator"></param>
    /// <param name="description"></param>
    /// <param name="sourceTitle"></param>
    /// <param name="sourceLanguage"></param>
    /// <param name="sourceLink"></param>
    /// <returns></returns>
    public static BookMeta Create(
        Guid id,
        string title,
        string author,
        Guid creator,
        string? description,
        string? sourceTitle,
        string? sourceLanguage,
        string? sourceLink
        )
    {
        BookMeta item = new BookMeta() {
            Id = id,
            Title = title,
            Author = author,
            Creator = creator,
            Description = description,
            Source_Title = sourceTitle,
            Source_Language = sourceLanguage,
            Source_Link = sourceLink
        };
        return item;
    }

    public BookMeta SetTitle(string title)
    {
        Title = title;
        return this;
    }

    public BookMeta SetAuthor(string author)
    {
        Author = author;
        return this;
    }

    public BookMeta SetCreator(Guid creator)
    {
        Creator = creator;
        return this;
    }

    public BookMeta SetDescription(string description)
    {
        Description = description;
        return this;
    }

    public BookMeta SetSourceTitle(string sourceTitle)
    {
        Source_Title = sourceTitle;
        return this;
    }

    public BookMeta SetSourceLanguage(string sourceLanguage)
    {
        Source_Language = sourceLanguage;
        return this;
    }

    public BookMeta SetSourceLink(string sourceLink)
    {
        Source_Link = sourceLink;
        return this;
    }

    public void SoftDelete()
    {
        IsDeleted = true;
        DeletionTime = DateTime.Now;
    }
}
