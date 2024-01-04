using Cloudea.Infrastructure.Models;
using Cloudea.Infrastructure.Utils;
using Cloudea.Service.Auth.Domain.Abstractions;
using Cloudea.Service.Auth.Domain.Entities;
using Cloudea.Service.Auth.Domain.Models;
using Cloudea.Service.Auth.Domain.Utils;
using Cloudea.Service.Base.Jwt;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Cloudea.Service.Auth.Domain.Applications
{
    public class UserDomainService
    {
        private readonly VerificationCodeService _userVerificationCodeService;
        private readonly JwtTokenService _jwtTokenService;
        private readonly IUserRepository _userRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;

        public UserDomainService(
            IUserRepository userRepository,
            IConfiguration configuration,
            IMemoryCache memoryCache,
            VerificationCodeService userVerificationCodeService,
            JwtTokenService jwtTokenService,
            IUserRoleRepository userRoleRepository,
            IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _memoryCache = memoryCache;
            AES_KEY = configuration["Secrets:AESKEY"];
            _userVerificationCodeService = userVerificationCodeService;
            _jwtTokenService = jwtTokenService;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// AES
        /// </summary>
        private readonly string AES_KEY;

        private static string HashPassword(string password, string salt)
        {
            return EncryptionUtils.EncryptMD5("Cloudea" + password + "system" + salt);
        }

        /// <summary>
        /// 检查用户是否已经注册
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        private async Task<bool> CheckUserRegistered([EmailAddress] string email)
        {
            var userExist = await _userRepository.CheckUserRegisteredAsync(email);
            return userExist;
        }

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
        public async Task<Result<User>> Register(string registerToken, string userName, string password)
        {
            // 验证信息是否为空
            if (userName == null ||
                registerToken == null ||
                password == null) {
                return Result.Fail("信息不能为空");
            }

            // 验证token有效性,并提取邮箱信息
            string userEmail;
            if (string.IsNullOrWhiteSpace(registerToken)) {
                return Result.Fail("token不能为空");
            }
            // 解析token
            try {
                string tokenData = EncryptionUtils.AESDecrypt(registerToken, AES_KEY);
                userEmail = tokenData.Split(",")[0];
            }
            catch (Exception ex) {
                //_logger.LogError(ex, message: "FinishRegister:" + ex.Message);
                return Result.Fail("token 解析错误");
            }

            // 验证信息合法性
            if (string.IsNullOrEmpty(password)) {
                return Result.Fail("密码不能为空");
            }
            if (password.Length < 6) {
                return Result.Fail("密码不能小于6位");
            }

            if (string.IsNullOrEmpty(userName)) {
                return Result.Fail("名称不能为空");
            }

            if (await CheckUserRegistered(userEmail)) {
                return Result.Fail("该邮箱已被注册");
            }

            // 创建新用户信息
            var nickName = "新用户";
            var salt = Guid.NewGuid().ToString("N");// 生成Salt
            var passwordHash = HashPassword(password, salt);// 加密密码
            var newUser = User.Create(
                userName, nickName, userEmail, passwordHash, salt, true);

            // 返回登录token
            return Result.Success(newUser);
        }

        /// <summary>
        /// 使用邮箱注册 - 生成注册token
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string GenerateRegisterToken(string email)
        {
            string token = EncryptionUtils.AESEncrypt($"{email},{DateTime.Now.Ticks}", AES_KEY);
            return token;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Result<string>> Login(UserLoginRequest request)
        {
            Guid userId;
            //用户名+密码登录
            if (request.LoginType == LoginType.UserNamePassword) {
                if (string.IsNullOrWhiteSpace(request.UserName)) {
                    return Result.Fail("用户名不能为空");
                }
                if (string.IsNullOrWhiteSpace(request.Password)) {
                    return Result.Fail("密码不能为空");
                }

                userId = await _userRepository.GetUserIdByUserName(request.UserName);
                if (userId == Guid.Empty) {
                    return HandleLoginFailure();
                }

                var checkRes = await _userRepository.CheckPassword(userId, request.Password);
                if (checkRes == false) {
                    return HandleLoginFailure();
                }
            }
            // 邮箱+密码登录
            else if (request.LoginType == LoginType.EmailPassword) {
                if (string.IsNullOrWhiteSpace(request.Email)) {
                    return Result.Fail("邮箱不能为空");
                }
                if (string.IsNullOrWhiteSpace(request.Password)) {
                    return Result.Fail("密码不能为空");
                }

                userId = await _userRepository.GetUserIdByEmail(request.Email);
                if (userId == Guid.Empty) {
                    return HandleLoginFailure();
                }

                var userRes = await _userRepository.CheckPassword(userId, request.Password);
                if (userRes is false) {
                    return HandleLoginFailure();
                }
            }
            // 邮箱+验证码登录
            else {
                if (string.IsNullOrWhiteSpace(request.Email)) {
                    return Result.Fail("邮箱不能为空");
                }
                if (string.IsNullOrWhiteSpace(request.Vercode)) {
                    return Result.Fail("验证码不能为空");
                }

                userId = await _userRepository.GetUserIdByEmail(request.Email);
                if (userId == Guid.Empty) {
                    return HandleLoginFailure();
                }
                var userRes = await _userRepository.CheckPassword(userId, request.Vercode);
                if (userRes == false) {
                    return HandleLoginFailure();
                }
            }
            //检查账号安全状态
            if (await _userRepository.CheckUserUnenableOrDeleted(userId)) {
                return HandleLoginFailure();
            }
            return await GenerateUserLoginToken(userId);
        }

        private static Result HandleLoginFailure()
        {
            return Result.Fail("登陆失败. 填写信息有误");
        }

        /// <summary>
        /// 生成登录token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Result<string>> GenerateUserLoginToken(Guid userId)
        {
            // 用户接口权限
            List<Claim> claims = new List<Claim>();
            HashSet<string> permissions = await GetPermissionsAsync(userId).ConfigureAwait(true);
            foreach (var permission in permissions) {
                claims.Add(new(JwtClaims.USER_PERMISSIONS, permission));
            }

            // UserId
            claims.Add(new Claim(JwtClaims.USER_ID, userId.ToString()));

            // 唯一登录标记
            claims.Add(new Claim(JwtClaims.USER_LOGIN_GUID, GenerateUserLoginGuid(userId.ToString())));
            var tokenRes = _jwtTokenService.Generate(claims);
            return tokenRes;
        }

        /// <summary>
        /// 获取PermissionId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<HashSet<string>> GetPermissionsAsync(Guid userId)
        {
            // 获取用户Role列表
            var roles = await _userRoleRepository.ReadByUserId(userId);

            // 获取用户Permission列表
            ICollection<Permission> permissionIds = new List<Permission>();
            foreach (var role in roles.Data) {
                var permissionList = await _roleRepository.GetRole(role);
                foreach (var permission in permissionList.Data.Permissions) {
                    permissionIds.Add(permission);
                }
            }
            var res = from permission in permissionIds
                      select permission.ToString();

            return res.ToHashSet();
        }

        #region 用户登录Guid控制
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
            _memoryCache.Set(CACHE_USER_LOGIN_GUID_KEY + userId, guid, TimeSpan.FromDays(1));
        }
        #endregion
    }
}
