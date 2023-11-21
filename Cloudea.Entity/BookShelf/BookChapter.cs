using Cloudea.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Entity.BookShelf
{
    [AutoGenerateTable]
    public class BookChapter : BaseEntity
    {
        /// <summary>
        /// 书本id
        /// </summary>
        public long book_id { get; set; }
        /// <summary>
        /// 章节号
        /// </summary>
        public string chapter_id { get; set; }
        /// <summary>
        /// 章节名字
        /// </summary>
        public string chapter_name { get; set; }
        /// <summary>
        /// 文件地址
        /// </summary>
        public string src { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        public long creator { get; set; }
    }
}
