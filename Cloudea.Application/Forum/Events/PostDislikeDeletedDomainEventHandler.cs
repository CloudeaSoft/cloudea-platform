using Cloudea.Application.Abstractions.Messaging;
using Cloudea.Domain.Forum.DomainEvents;
using Cloudea.Domain.Forum.Repositories;

namespace Cloudea.Application.Forum.Events
{
    public class PostDislikeDeletedDomainEventHandler
        : IDomainEventHandler<PostDislikeDeletedDomainEvent>
    {
        private readonly IForumPostRepository _postRepository;

        public PostDislikeDeletedDomainEventHandler(IForumPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task Handle(PostDislikeDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(notification.PostId, cancellationToken);
            if (post is null)
            {
                return;
            }

            post.DecreaseDislikeCount();
            _postRepository.Update(post);
        }
    }
}
