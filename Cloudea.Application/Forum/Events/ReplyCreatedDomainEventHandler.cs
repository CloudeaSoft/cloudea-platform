using Cloudea.Application.Abstractions.Messaging;
using Cloudea.Domain.Forum.DomainEvents;
using Cloudea.Domain.Forum.Repositories;

namespace Cloudea.Application.Forum.Events
{
    public class ReplyCreatedDomainEventHandler
        : IDomainEventHandler<ReplyCreatedDomainEvent>
    {
        private readonly IForumPostRepository _forumPostRepository;

        public ReplyCreatedDomainEventHandler(IForumPostRepository forumPostRepository)
        {
            _forumPostRepository = forumPostRepository;
        }

        public async Task Handle(ReplyCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var post = await _forumPostRepository.GetByIdAsync(notification.PostId, cancellationToken);
            if (post is null)
            {
                return;
            }
            post.IncreaseReplyCount();
            _forumPostRepository.Update(post);
        }
    }
}
