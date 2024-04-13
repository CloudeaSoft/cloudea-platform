using Cloudea.Application.Abstractions.Messaging;
using Cloudea.Domain.Forum.DomainEvents;
using Cloudea.Domain.Forum.Repositories;

namespace Cloudea.Application.Forum.Events
{
    public class PostHistoryCreatedDomainEventHandler
    : IDomainEventHandler<PostHistoryCreatedDomainEvent>
    {
        private readonly IForumPostRepository _forumPostRepository;

        public PostHistoryCreatedDomainEventHandler(IForumPostRepository forumPostRepository)
        {
            _forumPostRepository = forumPostRepository;
        }

        public async Task Handle(PostHistoryCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var post = await _forumPostRepository.GetByIdAsync(notification.PostId, cancellationToken);
            if (post is null)
            {
                return;
            }
            post.IncreaseClickCount();
            _forumPostRepository.Update(post);
        }
    }
}
