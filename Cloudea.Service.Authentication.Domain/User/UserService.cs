using Cloudea.Entity.Base.User;
using Cloudea.Infrastructure.Database;
using Cloudea.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using Cloudea.Infrastructure.Utils;
using Cloudea.Service.Auth.Domain.User.Models;

namespace Cloudea.Service.Auth.Domain.User
{
    public class UserService : BaseCurdService<Base_User>
    {
        private readonly ILogger<UserService> _logger;

        public UserService(IFreeSql database, ILogger<UserService> logger) : base(database)
        {
            _logger = logger;
            Database = database;
        }

        private string HashPassword(string password, string salt)
        {
            return EncryptionUtils.EncryptMD5("Cloudea" + password + "system" + salt);
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override Task<Result<long>> Create(Base_User entity)
        {
            // 补充信息
            entity.Enable = true;
            entity.DeleteMark = false;
            entity.Salt = Guid.NewGuid().ToString("N");// 生成Salt
            entity.Password = HashPassword(entity.Password, entity.Salt);// 加密密码

            return base.Create(entity);
        }

        /// <summary>
        /// 查询用户 Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Base_User> ReadUserById(long userId)
        {
            return await Database.Select<Base_User>().Where(t => t.Id == userId).FirstAsync();
        }

        /// <summary>
        /// 查询用户 Email
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public async Task<Base_User> ReadUserByEmail(string Email)
        {
            return await Database.Select<Base_User>().Where(t => t.Email == Email).FirstAsync();
        }

        /// <summary>
        /// 查询用户 Username
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public async Task<Base_User> ReadUserByUserName(string UserName)
        {
            return await Database.Select<Base_User>().Where(t => t.UserName == UserName).FirstAsync();
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public async Task<Result> UpdateUserProfile(UserProfile userProfile)
        {
            using (var uow = Database.CreateUnitOfWork()) {
                await uow.Orm.Update<Base_User>()
                    .Set(t => t.NickName, userProfile.NickName)
                    .Set(t => t.Avatar, userProfile.Avatar)
                    .Where(t => t.Id == userProfile.Id)
                    .ExecuteAffrowsAsync();
                uow.Commit();
            }

            return Result.Success();
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public override async Task<Result> Delete(long userId)
        {
            // 记录删除操作
            // 将DeleteMark修改为true
            await Database.Update<Base_User>().Where(x => x.Id == userId).Set(x => x.DeleteMark == true).ExecuteAffrowsAsync();

            return Result.Success();
        }

        /// <summary>
        /// 恢复用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Result> UnDelete(long userId)
        {
            // 记录恢复操作
            // 将DeleteMark修改为false
            await Database.Update<Base_User>().Where(x => x.Id == userId).Set(x => x.DeleteMark == false).ExecuteAffrowsAsync();

            return Result.Success();
        }

        /// <summary>
        /// 启用用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Result> Enable(long userId)
        {
            await Database.Update<Base_User>().Where(x => x.Id == userId).Set(x => x.Enable == true).ExecuteAffrowsAsync();
            return Result.Success();
        }

        /// <summary>
        /// 禁用用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Result> Disable(long userId)
        {
            await Database.Update<Base_User>().Where(x => x.Id == userId).Set(x => x.Enable == false).ExecuteAffrowsAsync();
            return Result.Success();
        }

        /// <summary>
        /// 检查密码是否正确
        /// </summary>
        /// <param name="mobilePhone"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<Result> CheckPassword(long userId, string password)
        {
            var user = await ReadUserById(userId);
            if (user == null) {
                return Result.Fail("用户不存在");
            }
            if (HashPassword(password, user.Salt) == user.Password) {
                return Result.Success();
            }
            return Result.Fail("密码错误");
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public async Task<Result> SetPassword(long userId, string newPassword)
        {
            var user = await Database.Select<Base_User>().Where(t => t.Id == userId).FirstAsync();
            if (user == null) {
                return Result.Fail("用户不存在");
            }
            if (string.IsNullOrEmpty(newPassword)) {
                return Result.Fail("密码不能为空");
            }
            if (newPassword.Length < 6) {
                return Result.Fail("密码不能小于6位");
            }

            user.Salt = Guid.NewGuid().ToString("N");
            user.Password = HashPassword(newPassword, user.Salt);

            await Database.Update<Base_User>()
                .Set(t => t.Salt, user.Salt)
                .Set(t => t.Password, user.Password)
                .Where(t => t.Id == userId)
                .ExecuteAffrowsAsync();
            return Result.Success();
        }
    }
}
