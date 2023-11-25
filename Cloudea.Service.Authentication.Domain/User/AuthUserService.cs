using Cloudea.Entity.Base.User;
using Cloudea.Infrastructure.Models;
using Cloudea.Infrastructure.Utils;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Cloudea.Service.Base.Jwt;
using Cloudea.Service.Auth.Domain.User.Models;
using Cloudea.Service.Auth.Domain.Authentication.Models;
using Cloudea.Service.Auth.Domain.Abstractions;

namespace Cloudea.Service.Auth.Domain.User
{
    public class AuthUserService
    {
        private readonly UserService _userService;

        private readonly VerificationCodeService _userVerificationCodeService;

        private readonly JwtTokenService _jwtTokenService;

        private readonly IPermissionService _permissionService;

        private readonly ILogger<AuthUserService> _logger;

        private readonly IFreeSql _database;

        private readonly IMemoryCache _memoryCache;

        public AuthUserService(
            UserService userService,
            VerificationCodeService userVerificationCodeService,
            ILogger<AuthUserService> logger,
            IFreeSql database,
            JwtTokenService jwtTokenService,
            IMemoryCache memoryCache,
            IConfiguration configuration,
            IPermissionService permissionService)
        {
            _userService = userService;
            _userVerificationCodeService = userVerificationCodeService;
            _logger = logger;
            _database = database;
            _jwtTokenService = jwtTokenService;
            _memoryCache = memoryCache;
            AES_KEY = configuration["Secrets:AESKEY"];
            _permissionService = permissionService;
        }

        /// <summary>
        /// AES
        /// </summary>
        private readonly string AES_KEY;

        /// 注册流程
        /// 1. 用户通过前端填写邮箱，并点击发送验证码按钮，获取验证码
        /// 2. 填写验证码并点击检查，前端提交信息到StartRegister()，得到注册token
        /// 3. 在新页面中填写个人信息，用户点击注册按钮后，前端将填写的个人信息与先前得到的注册token一并提交给后端

        /// <summary>
        /// 使用邮箱注册 - 检查邮箱合法性
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="verCode">验证码</param>
        /// <returns></returns>
        public async Task<Result<string>> StartRegister(string email, string verCode)
        {
            // 检查用户是否已经注册过了
            if (await CheckUserRegistered(email)) {
                return Result.Fail("该邮箱已被注册");
            }

            // 检查验证码有效性
            var checkRes = await _userVerificationCodeService.CheckVerCodeEmail(email, VerificationCodeType.RegisterByEmail, verCode);
            if (checkRes.Status is false) {
                return Result.Fail(checkRes.Message);
            }

            // 生成并返回注册Token
            return Result<string>.Success(GenerateRegisterToken(email));
        }

        /// <summary>
        /// 使用邮箱注册 - 完成注册流程 (需要注册token)
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<Result<string>> FinishRegister(UserRegisterRequest user)
        {
            // 验证user是否为空
            if (user == null) {
                return Result.Fail("信息不能为空");
            }

            // 验证token有效性,并提取邮箱信息
            string userEmail;
            if (string.IsNullOrWhiteSpace(user.RegisterToken)) {
                return Result.Fail("token不能为空");
            }
            else {
                try {
                    string tokenData = EncryptionUtils.AESDecrypt(user.RegisterToken, AES_KEY);
                    userEmail = tokenData.Split(",")[0];
                }
                catch (Exception ex) {
                    _logger.LogError(ex, message: "FinishRegister:" + ex.Message);
                    return Result.Fail("token 解析错误");
                }
            }
            // 验证信息合法性
            if (string.IsNullOrEmpty(user.Password)) {
                return Result.Fail("密码不能为空");
            }
            else {
                if (user.Password.Length < 6) {
                    return Result.Fail("密码不能小于6位");
                }
            }
            if (string.IsNullOrEmpty(user.UserName)) {
                return Result.Fail("名称不能为空");
            }

            if (await CheckUserRegistered(userEmail)) {
                return Result.Fail("该邮箱已被注册");
            }

            // 创建用户
            long userId;
            using (var uow = _database.CreateUnitOfWork()) {
                var res = await _userService.Create(new Base_User() {
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password
                });
                if (res.Status is false) {
                    return Result.Fail(res.Message);
                }
                uow.Commit();
                userId = res.Data;
            }

            // 返回登录token
            return await GenerateUserLoginToken(userId);
        }

        /// <summary>
        /// 检查用户是否已经注册
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        private async Task<bool> CheckUserRegistered(string email)
        {
            var user = await _userService.FindEntityByWhere(t => t.Email == email && t.DeleteMark == false);
            if (user != null) {
                return true;
            }
            else {
                return false;
            }
        }

        /// <summary>
        /// 使用邮箱注册 - 生成注册token
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string GenerateRegisterToken(string email)
        {
            string id = EncryptionUtils.AESEncrypt($"{email},{DateTime.Now.Ticks}", AES_KEY);
            return id;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Result<string>> Login(UserLoginRequest request)
        {
            // 数据校验
            if (request == null) {
                return Result.Fail("信息不能为空");
            }
            if (request.LoginType == null) {
                return Result.Fail("信息不能为空");
            }

            long userId;
            //用户名+密码登录
            if (request.LoginType == LoginType.UserNamePassword) {
                if (string.IsNullOrWhiteSpace(request.UserName)) {
                    return Result.Fail("用户名不能为空");
                }
                if (string.IsNullOrWhiteSpace(request.Password)) {
                    return Result.Fail("密码不能为空");
                }

                var user = await _userService.ReadUserByUserName(request.UserName);
                var userRes = await _userService.CheckPassword(user.Id, request.Password);
                if (userRes.Status == false) {
                    return Result.Fail("登录失败");
                }
                userId = user.Id;
            }
            // 邮箱+密码登录
            else if (request.LoginType == LoginType.EmailPassword) {
                if (string.IsNullOrWhiteSpace(request.Email)) {
                    return Result.Fail("邮箱不能为空");
                }
                if (string.IsNullOrWhiteSpace(request.Password)) {
                    return Result.Fail("密码不能为空");
                }

                var user = await _userService.ReadUserByEmail(request.Email);
                if (user is null) {
                    return Result.Fail("登陆失败");
                }

                var userRes = await _userService.CheckPassword(user.Id, request.Password);
                if (userRes.Status is false) {
                    return Result.Fail("登录失败");
                }
                userId = user.Id;
            }
            // 邮箱+验证码登录
            else {
                if (string.IsNullOrWhiteSpace(request.Email)) {
                    return Result.Fail("邮箱不能为空");
                }
                if (string.IsNullOrWhiteSpace(request.Vercode)) {
                    return Result.Fail("验证码不能为空");
                }

                var user = await _userService.ReadUserByEmail(request.Email);
                var userRes = await _userService.CheckPassword(user.Id, request.Vercode);
                if (userRes.Status == false) {
                    return Result.Fail("登录失败");
                }
                userId = user.Id;
            }

            return await GenerateUserLoginToken(userId);
        }

        /// <summary>
        /// 生成登录token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Result<string>> GenerateUserLoginToken(long userId)
        {
            // 生成token
            List<Claim> claims = new List<Claim>();
            // 标记 用户接口权限
            HashSet<string> permissions = await _permissionService
                .GetPermissionsAsync(userId).ConfigureAwait(true);
            foreach (var permission in permissions) {
                claims.Add(new(JwtClaims.USER_PERMISSIONS, permission));
            }
            // UserId
            claims.Add(new Claim(JwtClaims.USER_ID, userId.ToString()));
            Console.WriteLine(userId);
            // 用于保证用户只能登录一个
            claims.Add(new Claim(JwtClaims.USER_LOGIN_GUID, GenerateUserLoginGuid(userId.ToString())));
            var tokenRes = _jwtTokenService.Generate(claims);
            return tokenRes;
        }

        /// <summary>
        /// 记录 登录 (每小时去重)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public void RecordLogin(long userId)
        {
            string time = DateTime.Now.Hour.ToString();
            string date = DateTime.Now.ToString("yyyy-MM-dd");

            string key = $"{LoginCheck.USER_LOGIN_PREFIX}{userId}{date}{time}";

            // 检查是否已保存
            if (_memoryCache.Get(key) != null) {
                Console.WriteLine("key" + key);
                return;
            }

            // 过期时间
            var ttl = DateTime.Parse(DateTime.Now.AddHours(1).ToString("yyyy-MM-dd HH:00:00"));

            // 设置一下缓存 保证下次进来的时候不访问数据库  下一个小时过期
            _memoryCache.Set(key, true, ttl);
        }

        #region 用户登录Guid
        const string CACHE_USER_LOGIN_GUID_KEY = $"{JwtClaims.USER_LOGIN_GUID}:";
        /// <summary>
        /// 生成用户唯一登录的guid
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private string GenerateUserLoginGuid(string userId)
        {
            var guid = Guid.NewGuid().ToString("N");
            SetUserLoginGuid(userId, guid);
            return guid;
        }

        /// <summary>
        /// 获得用户当前登录guid
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserLoginGuid(string userId)
        {
            Console.WriteLine("get:" + userId);
            if (_memoryCache.TryGetValue(CACHE_USER_LOGIN_GUID_KEY + userId, out string guid)) {
                return guid;
            }
            return "";
        }
        /// <summary>
        /// 设置用户登录的guid
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="guid"></param>
        public void SetUserLoginGuid(string userId, string guid)
        {
            Console.WriteLine("set:" + userId);
            _memoryCache.Set(CACHE_USER_LOGIN_GUID_KEY + userId, guid, TimeSpan.FromDays(1));
            Console.WriteLine("save:" + guid);
        }
        #endregion
    }
}