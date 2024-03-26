using Cloudea.Application.Abstractions.Messaging;
using Cloudea.Domain.Common.Repositories;
using Cloudea.Domain.Identity.DomainEvents;
using Cloudea.Domain.Identity.Entities;
using Cloudea.Domain.Identity.Repositories;
using Cloudea.Domain.Identity.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Cloudea.Application.Identity.Events
{
    public class CreateUserProfileWhenUserCreatedDomainEventHandler : IDomainEventHandler<UserCreatedDomainEvent>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateUserProfileWhenUserCreatedDomainEventHandler> _logger;

        public CreateUserProfileWhenUserCreatedDomainEventHandler(
            IUnitOfWork unitOfWork,
            IUserProfileRepository repository,
            IUserRepository userRepository,
            ILogger<CreateUserProfileWhenUserCreatedDomainEventHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _userProfileRepository = repository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(notification.UserId, cancellationToken);
            if (user is null) {
                var ex = new NullReferenceException($"EventId:{notification.Id}, UserId:{notification.UserId}, User.NotFound");
                _logger.LogCritical(ex.Message);
                throw ex;
            }
            var displayName = DisplayName.Create($"新用户_{GenerateRandomString(10)}");
            var profile = UserProfile.Create(user, displayName);
            _userProfileRepository.Add(profile);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
