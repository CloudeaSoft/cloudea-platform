using Cloudea.Application.Abstractions.Messaging;
using Cloudea.Domain.Forum.DomainEvents;
using Cloudea.Domain.Forum.Repositories;

namespace Cloudea.Application.Forum.Events
{
    public class PostCreatedDomainEventHandler
        : IDomainEventHandler<PostCreatedDomainEvent>
    {
        private readonly IForumSectionRepository _forumSectionRepository;

        public PostCreatedDomainEventHandler(IForumSectionRepository forumSectionRepository)
        {
            _forumSectionRepository = forumSectionRepository;
        }

        public async Task Handle(PostCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var section = await _forumSectionRepository.GetByIdAsync(notification.SectionId, cancellationToken);
            if (section is null)
            {
                return;
            }
            section.IncreaseTopicCount();
            _forumSectionRepository.Update(section);
        }
    }
}
