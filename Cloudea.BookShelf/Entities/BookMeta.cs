using Cloudea.Infrastructure.Database;
using FreeSql.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace Cloudea.Service.Book.Domain.Entities;

[AutoGenerateTable]
[Table(Name = "book_meta")]
public class BookMeta : BaseEntity, ISoftDelete
{
    /// <summary>
    /// 书名
    /// </summary>
    [Column(DbType = "varchar(128)")]
    public string Title { get; set; }
    /// <summary>
    /// 作者
    /// </summary>
    [Column(DbType = "varchar(16)")]
    public string Author { get; set; }
    /// <summary>
    /// 简介
    /// </summary>
    [StringLength(255)]
    [Column(DbType = "varchar(255)")]
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
    [Url]
    public string? Source_Link { get; set; }

    public bool IsDeleted { get; private set; }

    public DateTime? DeletionTime { get; private set; }

    /// <summary>
    /// 简单创建
    /// </summary>
    /// <param name="title"></param>
    /// <param name="author"></param>
    /// <param name="creator"></param>
    /// <returns></returns>
    public static BookMeta QuickCreate(
        Guid id,
        string title,
        string author,
        Guid creator)
    {
        BookMeta bookMeta = new BookMeta() {
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
        this.Title = title;
        return this;
    }

    public BookMeta SetAuthor(string author)
    {
        this.Author = author;
        return this;
    }

    public BookMeta SetCreator(Guid creator)
    {
        this.Creator = creator;
        return this;
    }

    public BookMeta SetDescription(string description)
    {
        this.Description = description;
        return this;
    }

    public BookMeta SetSourceTitle(string sourceTitle)
    {
        this.Source_Title = sourceTitle;
        return this;
    }

    public BookMeta SetSourceLanguage(string sourceLanguage)
    {
        this.Source_Language = sourceLanguage;
        return this;
    }

    public BookMeta SetSourceLink(string sourceLink)
    {
        this.Source_Link = sourceLink;
        return this;
    }

    public void SoftDelete()
    {
        this.IsDeleted = true;
        this.DeletionTime = DateTime.Now;
    }
}
