using Cloudea.Domain.Common.Repositories;
using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Identity.Entities;
using Cloudea.Domain.Identity.Repositories;

namespace Cloudea.Application.Identity
{
    public class UserProfileService
    {
        private readonly UserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileService(UserService userService, IUserProfileRepository userProfileRepository, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _userProfileRepository = userProfileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UserProfile>> GetUserProfileAsync(Guid userId)
        {
            var profile = await _userProfileRepository.GetByUserIdAsync(userId);
            if (profile is null) {
                return new Error("UserProfile.NotFound");
            }
            return profile;
        }
    }
}
