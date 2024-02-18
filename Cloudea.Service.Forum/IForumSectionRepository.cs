using Cloudea.Entity.Forum;
using Cloudea.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Forum.Domain
{
    /// <summary>
    /// Forum_Section仓储
    /// </summary>
    public interface IForumSectionRepository
    {
        /// <summary>
        /// 读取指定Id的Section信息
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        Task<Result<Forum_Section>> ReadSection(Guid sectionId);

        /// <summary>
        /// 读取Section列表
        /// </summary>
        /// <returns></returns>
        Task<Result<List<Forum_Section>>> ListSection();

        /// <summary>
        /// 创建Section
        /// </summary>
        /// <param name="newSection"></param>
        /// <returns></returns>
        Task<Result<long>> CreateSection(Forum_Section newSection);

        /// <summary>
        /// 增加Section的帖子计数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        Task<Result> IncreaseTopicCount(Guid id);

        /// <summary>
        /// 减少Section的帖子计数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        Task<Result> DecreaseTopicCount(Guid id);
    }
}
