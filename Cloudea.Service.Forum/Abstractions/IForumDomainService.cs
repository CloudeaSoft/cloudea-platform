using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Forum.Domain.Abstractions
{
    public interface IForumDomainService
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        public void GetUserInfo(Guid userId);

        /// <summary>
        /// 获取帖子列表
        /// </summary>
        public void GetTopicList(long sectionId);
        
        /// <summary>
        /// 发帖
        /// </summary>
        public void PostTopic();
        /// <summary>
        /// 删帖
        /// </summary>
        public void DeleteTopic(long topicId);
        
        /// <summary>
        /// 回复贴
        /// </summary>
        public void PostReply();
        /// <summary>
        /// 删除回复贴
        /// </summary>
        public void DeleteReply();

        /// <summary>
        /// 获取帖子的指定页信息
        /// </summary>
        public void GetTopicPage(long topicId, int page = 1);
    }
}
