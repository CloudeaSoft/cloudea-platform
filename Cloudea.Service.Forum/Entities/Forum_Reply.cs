using Cloudea.Infrastructure.Database;
using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Entity.Forum
{
    [AutoGenerateTable]
    [Table(Name = "forum_reply")]
    [Index("uk_topic_reply", "TopicId,Id", true)]
    public record Forum_Reply : BaseDataEntity
    {
        /// <summary>
        /// 回复的主题帖ID
        /// </summary>
        public Guid TopicId { get; set; }
        /// <summary>
        /// 回复的回复贴ID
        ///     为空则不回复其他回复贴
        /// </summary>
        public Guid? ReplyId { get; set; }
        /// <summary>
        /// 用户ID - 发帖人
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 标题 - 不一定使用，预留字段
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }
}
