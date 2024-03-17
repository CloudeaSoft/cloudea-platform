using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Forum.Entities;

namespace Cloudea.Domain.Forum.Repositories
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
        Task<ForumSection?> GetByIdAsync(Guid sectionId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 读取Section列表
        /// </summary>
        /// <returns></returns>
        Task<List<ForumSection>> GetByIdListAsync(List<Guid> idList, CancellationToken cancellationToken = default);

        /// <summary>
        /// 读取Section列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PageResponse<ForumSection>> GetWithPageRequestAsync(PageRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// 创建Section
        /// </summary>
        /// <param name="newSection"></param>
        /// <returns></returns>
        void Add(ForumSection newSection);

        /// <summary>
        /// 更新Section
        /// </summary>
        /// <param name="newSection"></param>
        /// <returns></returns>
        void Update(ForumSection newSection);
    }
}
