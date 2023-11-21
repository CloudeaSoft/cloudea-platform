using Cloudea.Infrastructure.Database;

namespace Cloudea.Entity.BookShelf
{
    [AutoGenerateTable]
    public class BookMeta : BaseEntity
    {
        /// <summary>
        /// 书名
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string author { get; set; }
        /// <summary>
        /// 译者
        /// </summary>
        public string? translator { get; set; }
        /// <summary>
        /// 语言
        /// </summary>
        public string language { get; set; }
        /// <summary>
        /// 源语言 书名
        /// </summary>
        public string source_title { get; set; }
        /// <summary>
        /// 源语言
        /// </summary>
        public string source_language { get; set; }
        /// <summary>
        /// 源链接
        /// </summary>
        public string source_link { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string introduction { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        public long creator { get; set; }
    }
}
