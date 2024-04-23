using Cloudea.Application.Abstractions;
using Cloudea.Application.Identity.Contracts;
using Cloudea.Application.Utils;
using Cloudea.Domain.Common.Repositories;
using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Common.Utils;
using Cloudea.Domain.Identity.Entities;
using Cloudea.Domain.Identity.Enums;
using Cloudea.Domain.Identity.Models;
using Cloudea.Domain.Identity.Repositories;
using Cloudea.Domain.Identity.ValueObjects;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Cloudea.Application.Identity
{
    public class IdentityService
    {
        private readonly VerificationCodeService _userVerificationCodeService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUserRepository _userRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IReportRepository _reportRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IUnitOfWork _unitOfWork;

        public IdentityService(
            IUserRepository userRepository,
            IConfiguration configuration,
            IMemoryCache memoryCache,
            VerificationCodeService userVerificationCodeService,
            IJwtTokenService jwtTokenService,
            IUserRoleRepository userRoleRepository,
            IRoleRepository roleRepository,
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser,
            IReportRepository reportRepository)
        {
            _userRepository = userRepository;
            _memoryCache = memoryCache;
            AES_KEY = configuration["Secrets:AESKEY"]!;
            _userVerificationCodeService = userVerificationCodeService;
            _jwtTokenService = jwtTokenService;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _reportRepository = reportRepository;
        }

        /// <summary>
        /// AES
        /// </summary>
        private readonly string AES_KEY;

        /// <summary>
        /// 检查用户是否已经注册
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        private async Task<bool> CheckUserRegisteredAsync([EmailAddress] string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return user is not null;
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
        public async Task<Result<string>> GetRegisterTokenAsync(string email, string verCode)
        {
            // 检查用户是否已经注册过了
            if (await CheckUserRegisteredAsync(email))
            {
                return new Error("该邮箱已被注册");
            }

            // 检查验证码有效性
            var checkRes = await _userVerificationCodeService.CheckVerCodeEmail(email, VerificationCodeType.RegisterByEmail, verCode);
            if (checkRes.IsFailure)
            {
                return checkRes.Error;
            }

            // 生成并返回注册Token
            return Result.Success(GenerateRegisterToken(email));
        }

        /// <summary>
        /// 使用邮箱注册 - 完成注册流程 (需要注册token)
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<Result<Guid>> RegisterAsync(string registerToken, string userName, string password, CancellationToken cancellationToken = default)
        {
            // 验证信息是否为空
            if (userName == null ||
                registerToken == null ||
                password == null)
            {
                return new Error("信息不能为空");
            }

            // 验证token有效性,并提取邮箱信息
            string userEmail;
            if (string.IsNullOrWhiteSpace(registerToken))
            {
                return new Error("token不能为空");
            }
            // 解析token
            try
            {
                string tokenData = EncryptionUtils.AESDecrypt(registerToken, AES_KEY);
                userEmail = tokenData.Split(",")[0];
            }
            catch (Exception ex)
            {
                return new Error("User.Token.InvalidParam", ex.ToString());
            }

            // 验证信息合法性
            if (string.IsNullOrEmpty(password))
            {
                return new Error("密码不能为空");
            }
            if (password.Length < 6)
            {
                return new Error("密码不能小于6位");
            }

            if (string.IsNullOrEmpty(userName))
            {
                return new Error("名称不能为空");
            }

            if (await CheckUserRegisteredAsync(userEmail))
            {
                return new Error("该邮箱已被注册");
            }

            // 创建新用户信息
            var passwordRes = Password.Create(password);
            if (passwordRes.IsFailure)
            {
                return passwordRes.Error;
            }

            var newSalt = GenerateNewSalt();// generate salt

            var passwordHash = HashPassword(passwordRes.Data, newSalt);

            var newUser = User.Create(
                userName,
                userEmail,
                passwordHash,
                newSalt,
                true);

            _userRepository.Add(newUser);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            // return login token
            return newUser.Id;
        }

        private static PasswordHash HashPassword(Password password, Salt salt)
        {
            return PasswordHash.Create(EncryptionUtils.EncryptMD5("Cloudea" + password.Value + "System" + salt.Value)).Data;
        }

        private static bool CheckPassword(Password password, User user)
        {
            if (!HashPassword(password, user.Salt).Equals(user.PasswordHash))
            {
                return false;
            }
            return true;
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
        public async Task<Result<string>> LoginAsync(UserLoginRequest request, CancellationToken cancellationToken = default)
        {
            User? user;
            // 邮箱+验证码登录
            if (request.LoginType == LoginType.EmailVercode)
            {
                if (string.IsNullOrWhiteSpace(request.Email))
                {
                    return new Error("邮箱不能为空");
                }
                if (string.IsNullOrWhiteSpace(request.Vercode))
                {
                    return new Error("验证码不能为空");
                }
                user = await _userRepository.GetByEmailAsync(request.Email);
            }
            //用户名+密码登录
            else if (request.LoginType == LoginType.UserNamePassword)
            {
                if (string.IsNullOrWhiteSpace(request.UserName))
                {
                    return new Error("用户名不能为空");
                }
                if (string.IsNullOrWhiteSpace(request.Password))
                {
                    return new Error("密码不能为空");
                }

                user = await _userRepository.GetByUserNameAsync(request.UserName);
            }
            // 邮箱+密码登录
            else
            {
                if (string.IsNullOrWhiteSpace(request.Email))
                {
                    return new Error("邮箱不能为空");
                }
                if (string.IsNullOrWhiteSpace(request.Password))
                {
                    return new Error("密码不能为空");
                }
                user = await _userRepository.GetByEmailAsync(request.Email);
            }

            if (user is null)
            {
                return HandleLoginFailure();
            }
            if (user.Enable is false)
            {
                return HandleLoginFailure();
            }

            if (request.LoginType == LoginType.EmailVercode)
            {
                return HandleLoginFailure();
            }
            else if (CheckPassword(Password.Create(request.Password!).Data, user) is false)
            {
                return HandleLoginFailure();
            }

            return await GenerateUserLoginTokenAsync(user.Id, cancellationToken);
        }



        private static Result<string> HandleLoginFailure()
        {
            return new Error("User.LoginFailure", "安全信息验证失败");
        }

        /// <summary>
        /// 生成登录token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Result<string>> GenerateUserLoginTokenAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            // 用户接口权限
            List<Claim> claims = [];
            HashSet<string> permissions = await GetPermissionsAsync(userId);
            foreach (var permission in permissions)
            {
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
        /// 修改密码
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<string>> ChangePasswordAsync(
            ChangePasswordRequest request,
            CancellationToken cancellationToken = default)
        {
            // init
            var user = await _currentUser.GetUserInfoAsync(cancellationToken);
            if (user is null)
            {
                return new Error("User.NotFound");
            }

            // check password
            var oldPasswordRes = Password.Create(request.OldPassword);
            if (oldPasswordRes.IsFailure)
            {
                return new Error("User.Password.BadRequest");
            }
            var checkRes = CheckPassword(oldPasswordRes.Data, user);
            if (checkRes is false)
            {
                return new Error("User.Password.BadRequest");
            }

            // generate new password
            var newPasswordRes = Password.Create(request.NewPassword);
            if (newPasswordRes.IsFailure)
            {
                return new Error("User.Password.BadRequest", "密码格式不正确");
            }
            var newSalt = GenerateNewSalt();
            var newPasswordHash = HashPassword(newPasswordRes.Data, newSalt);
            user.SetPassword(newPasswordHash, newSalt);

            _userRepository.Update(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return await GenerateUserLoginTokenAsync(user.Id, cancellationToken);
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<string>> ResetPasswordAsync(
            ResetPasswordRequest request,
            CancellationToken cancellationToken = default)
        {
            // init
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user is null)
            {
                return new Error("User.NotFound");
            }

            // check vercode
            var vercodeRes = await _userVerificationCodeService.CheckVerCodeEmail(
                user.Email,
                VerificationCodeType.ResetPasswordByEmail,
                request.VerCode,
                cancellationToken: cancellationToken);

            if (vercodeRes.IsFailure)
            {
                return new Error("VerificationCode.NotFound");
            }

            // generate new password
            var newPasswordRes = Password.Create(request.NewPassword);
            if (newPasswordRes.IsFailure)
            {
                return new Error("User.Password.BadRequest", "密码格式不正确");
            }
            var newSalt = GenerateNewSalt();
            var newPasswordHash = HashPassword(newPasswordRes.Data, newSalt);
            user.SetPassword(newPasswordHash, newSalt);

            _userRepository.Update(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return await GenerateUserLoginTokenAsync(user.Id, cancellationToken);
        }

        public static Salt GenerateNewSalt()
        {
            return Salt.Create(Guid.NewGuid().ToString("N")).Data;
        }

        /// <summary>
        /// 获取PermissionId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<HashSet<string>> GetPermissionsAsync(Guid userId)
        {
            // 获取用户Role列表
            var roleList = await _userRoleRepository.GetRoleListByUserId(userId);

            // 获取用户Permission列表
            ICollection<Permission> permissionIds = new List<Permission>();
            foreach (var roleItem in roleList)
            {
                var role = await _roleRepository.GetByIdAsync(roleItem);
                if (role is null)
                {
                    continue;
                }
                foreach (var permission in role.Permissions)
                {
                    permissionIds.Add(permission);
                }
            }
            var res = from permission in permissionIds
                      select permission.ToString();

            return res.ToHashSet();
        }

        /// <summary>
        /// 封禁用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Result> BanUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 举报用户
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<Report>> CreateReportAsync(CreateReportRequest request, CancellationToken cancellationToken = default)
        {
            var userId = await _currentUser.GetUserIdAsync();

            var report = Report.Create(
                userId,
                request.TargetItemId,
                request.TargetItemId,
                request.TargetItemType,
                request.Description);

            if (report is null)
            {
                return new Error("Identity.Report.InvalidParam");
            }

            _reportRepository.Add(report);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return report;
        }

        /// <summary>
        /// 读取一页举报记录
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<PageResponse<Report>>> GetReportPageAsync(PageRequest request, CancellationToken cancellationToken = default)
        {
            var userId = await _currentUser.GetUserIdAsync();

            var response = await _reportRepository.ListByUserIdPageRequestAsync(userId, request, cancellationToken);

            return response;
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
        public string? GetUserLoginGuid(string userId)
        {
            if (_memoryCache.TryGetValue(CACHE_USER_LOGIN_GUID_KEY + userId, out string? guid))
            {
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
