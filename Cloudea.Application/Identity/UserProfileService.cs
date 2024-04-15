﻿using Cloudea.Application.Abstractions;
using Cloudea.Application.File;
using Cloudea.Domain.Common.Repositories;
using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Identity.Entities;
using Cloudea.Domain.Identity.Repositories;
using Microsoft.AspNetCore.Http;

namespace Cloudea.Application.Identity
{
    public class UserProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ICurrentUser _currentUser;
        private readonly UserService _userService;
        private readonly FileService _fileService;

        public UserProfileService(UserService userService, IUserProfileRepository userProfileRepository, IUnitOfWork unitOfWork, ICurrentUser currentUser, FileService fileService)
        {
            _userService = userService;
            _userProfileRepository = userProfileRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _fileService = fileService;
        }

        public async Task<Result<UserProfile>> GetUserProfileAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var profile = await _userProfileRepository.GetByUserIdAsync(userId);
            if (profile is null)
            {
                return new Error("UserProfile.NotFound");
            }
            return profile;
        }

        public async Task<Result<UserProfile>> GetSelfUserProfileAsync(CancellationToken cancellationToken = default)
        {
            var profile = await _userProfileRepository.GetByUserIdAsync(await _currentUser.GetUserIdAsync());
            if (profile is null)
            {
                return new Error("UserProfile.NotFound");
            }
            return profile;
        }

        public async Task<Result<UserProfile>> CreateUserProfileAvatar(IFormFile file, CancellationToken cancellationToken = default)
        {
            // 检查用户
            Guid userId = await _currentUser.GetUserIdAsync();
            if (userId == Guid.Empty)
            {
                return new Error("UserProfile.NotFound");
            }
            UserProfile? profile = await _userProfileRepository.GetByUserIdAsync(userId, cancellationToken);
            if (profile is null)
            {
                return new Error("UserProfile.NotFound");
            }

            // 检测头像文件
            if (file == null || file.Length == 0)
            {
                return new Error("UserProfile.Avatar.InvalidParam", "请选择一个文件上传");
            }

            long maxSize = 1024 * 1024; // 1MB  
            if (file.Length > maxSize)
            {
                return new Error("UserProfile.Avatar.InvalidParam", "文件大小不能超过1MB");
            }

            var allowedTypes = new List<string> { "image/jpeg", "image/png" };
            var contentType = file.ContentType;
            if (!allowedTypes.Contains(contentType))
            {
                return new Error("UserProfile.Avatar.InvalidParam", "只允许上传JPEG或PNG格式的图片");
            }

            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            var allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(fileExtension))
            {
                return new Error("UserProfile.Avatar.InvalidParam", "只允许上传JPEG或PNG格式的图片");
            }

            // 上传头像
            var stream = file.OpenReadStream();
            var res = await _fileService.UploadFileAsync(stream, file.FileName, cancellationToken: cancellationToken);
            if (res.IsFailure)
            {
                return res.Error;
            }            

            // 保存头像
            profile.SetAvatar(res.Data.RemoteUrl);
            _userProfileRepository.Update(profile);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return profile;
        }
    }
}
