using Cloudea.Application.Abstractions.Messaging;
using Cloudea.Application.File;
using Cloudea.Domain.Identity.DomainEvents;

namespace Cloudea.Application.Identity.Events
{
    internal class UserProfileAvatarUpdatedDomainEventHandler
        : IDomainEventHandler<UserProfileAvatarUpdatedDomainEvent>
    {
        private readonly FileService _fileService;

        public UserProfileAvatarUpdatedDomainEventHandler(FileService fileService)
        {
            _fileService = fileService;
        }

        public async Task Handle(UserProfileAvatarUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var filePath = notification.OldAvatarUri;
            if (filePath is null)
            {
                return;
            }
            await _fileService.DeleteFileAsync(filePath, cancellationToken);
        }
    }
}
