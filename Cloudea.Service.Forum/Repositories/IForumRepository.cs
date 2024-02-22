using Cloudea.Entity.Forum;
using Cloudea.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Forum.Domain.Repositories
{
    public interface IForumRepository
    {
        /// <summary>
        /// 创建Forum_Topic：
        ///     1. 插入Forum_Topic
        ///     2. 对应的Forum_Section帖子计数增加
        ///     3. 成功则返回TopicID，失败则返回错误原因
        /// </summary>
        /// <returns></returns>
        Task<Result<Forum_Topic>> SaveTopicAsync(Forum_Topic topic);
    }
}
