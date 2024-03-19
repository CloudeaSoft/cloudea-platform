using Cloudea.Domain.Identity.Entities;

namespace Cloudea.Domain.Identity.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);

        void Update(User user);

        /// <summary>
        /// 查询用户 使用UserId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询用户 使用Email
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        Task<User?> GetByEmailAsync(string Email, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询用户 使用Username
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        Task<User?> GetByUserNameAsync(string UserName, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询全部用户
        /// </summary>
        /// <returns></returns>
        Task<List<Guid>> ListAllUserIdAsync();
    }
}
