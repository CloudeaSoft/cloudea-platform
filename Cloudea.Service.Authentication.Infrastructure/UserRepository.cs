using Cloudea.Infrastructure.Database;
using Cloudea.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using Cloudea.Infrastructure.Utils;
using Cloudea.Service.Auth.Domain.Models;
using Cloudea.Service.Auth.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using Cloudea.Service.Auth.Domain.Abstractions;

namespace Cloudea.Service.Auth.Domain
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(IFreeSql database, ILogger<UserRepository> logger) : base(database)
        {
            _logger = logger;
            _database = database;
        }

        private string HashPassword(string password, string salt)
        {
            return EncryptionUtils.EncryptMD5("Cloudea" + password + "system" + salt);
        }

        /// <summary>
        /// 查询用户 Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> ReadUserById(Guid id)
        {
            return await _database.Select<User>().Where(t => t.Id == id).FirstAsync();
        }

        /// <summary>
        /// 查询用户 Email
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public async Task<User> ReadUserByEmail(string Email)
        {
            return await _database.Select<User>().Where(t => t.Email == Email).FirstAsync();
        }

        /// <summary>
        /// 查询用户 Username
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public async Task<User> ReadUserByUserName(string UserName)
        {
            return await _database.Select<User>().Where(t => t.UserName == UserName).FirstAsync();
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public async Task<Result> UpdateUserProfile(UserProfile userProfile)
        {
            using (var uow = _database.CreateUnitOfWork()) {
                await uow.Orm.Update<User>()
                    .Set(t => t.NickName, userProfile.NickName)
                    .Set(t => t.Avatar, userProfile.Avatar)
                    .Where(t => t.Id == userProfile.Id)
                    .ExecuteAffrowsAsync();
                uow.Commit();
            }

            return Result.Success();
        }

        /// <summary>
        /// 禁用用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Result> Disable(Guid userId)
        {
            await _database.Update<User>().Where(x => x.Id == userId).Set(x => x.Enable == false).ExecuteAffrowsAsync();
            return Result.Success();
        }

        /// <summary>
        /// 检查密码是否正确
        /// </summary>
        /// <param name="mobilePhone"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> CheckPassword(Guid userId, string password)
        {
            var user = await ReadUserById(userId);
            if (user == null) {
                return false;
            }
            if (HashPassword(password, user.Salt) == user.PasswordHash) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public async Task<Result> SetPassword(Guid userId, string newPassword)
        {
            var user = await _database.Select<User>().Where(t => t.Id == userId).FirstAsync();
            if (user == null) {
                return Result.Failure(new Error("用户不存在"));
            }
            if (string.IsNullOrEmpty(newPassword)) {
                return Result.Failure(new Error("密码不能为空"));
            }
            if (newPassword.Length < 6) {
                return Result.Failure(new Error("密码不能小于6位"));
            }

            user.Salt = Guid.NewGuid().ToString("N");
            user.PasswordHash = HashPassword(newPassword, user.Salt);

            await _database.Update<User>()
                .Set(t => t.Salt, user.Salt)
                .Set(t => t.PasswordHash, user.PasswordHash)
                .Where(t => t.Id == userId)
                .ExecuteAffrowsAsync();
            return Result.Success();
        }

        public async Task<bool> CheckUserRegisteredAsync([EmailAddress] string email)
        {
            var res = await _database.Select<User>().Where(t => t.Email == email).FirstAsync();
            if (res == null) {
                return false;
            }
            return true;
        }

        public async Task<Guid> GetUserIdByUserName(string username)
        {
            var res = await _database.Select<User>().Where(t => t.UserName == username).FirstAsync();
            if (res == null) {
                return Guid.Empty;
            }
            return res.Id;
        }

        public async Task<Guid> GetUserIdByEmail([EmailAddress] string email)
        {
            var user = await FindEntityByWhere(t =>
                t.Email == email &&
                t.IsDeleted == false);
            if (user != null) {
                return user.Id;
            }
            else {
                return Guid.Empty;
            }
        }

        public async Task<Result<User>> GetUser(Guid id)
        {
            var res = await Read(id);
            return res;
        }

        public async Task<bool> CheckUserUnenableOrDeleted(Guid id)
        {
            var user = await Read(id);
            if (!user.Data.Enable || user.Data.IsDeleted) {
                return true;
            }
            return false;
        }
    }
}
