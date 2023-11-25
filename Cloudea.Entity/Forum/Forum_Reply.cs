using Cloudea.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Entity.Forum
{
    public class Forum_Reply:BaseEntity
    {
        /// <summary>
        /// 回复帖ID
        /// </summary>
        public long TopicId {  get; set; }
        /// <summary>
        /// 是否回复 回复贴
        /// </summary>
        public bool IsSubReply { get; set; }
        /// <summary>
        /// 指向的回复贴ID
        /// </summary>
        public long ReplyId {  get; set; }
        /// <summary>
        /// 用户ID - 发帖人
        /// </summary>
        public long UserId { get; set; }
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
