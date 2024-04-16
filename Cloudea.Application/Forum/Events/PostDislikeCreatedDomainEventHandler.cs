using Cloudea.Application.Abstractions.Messaging;
using Cloudea.Domain.Forum.DomainEvents;
using Cloudea.Domain.Forum.Repositories;

namespace Cloudea.Application.Forum.Events
{
    internal class PostDislikeCreatedDomainEventHandler
        : IDomainEventHandler<PostDislikeCreatedDomainEvent>
    {
        private readonly IForumPostRepository _postRepository;

        public PostDislikeCreatedDomainEventHandler(IForumPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task Handle(PostDislikeCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(notification.PostId, cancellationToken);
            if (post is null)
            {
                return;
            }

            post.IncreaseDislikeCount();
            _postRepository.Update(post);
        }
    }
}
