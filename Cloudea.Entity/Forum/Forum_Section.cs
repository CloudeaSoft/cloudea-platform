using Cloudea.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Entity.Forum
{
    /// <summary>
    /// 论坛板块
    /// </summary>
    [AutoGenerateTable]
    public class Forum_Section : BaseEntity
    {
        /// <summary>
        /// 板块名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 板块主
        /// </summary>
        public long MasterId { get; set; }
        /// <summary>
        /// 板块简介
        /// </summary>
        public string Statement { get; set; }
        /// <summary>
        /// 板块点击次数
        /// </summary>
        public long ClickCount { get; set; }
        /// <summary>
        /// 板块主题帖数
        /// </summary>
        public long TopicCount {  get; set; }
    }
}
