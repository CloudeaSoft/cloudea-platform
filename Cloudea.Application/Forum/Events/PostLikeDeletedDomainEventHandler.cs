using Cloudea.Application.Abstractions.Messaging;
using Cloudea.Domain.Forum.DomainEvents;
using Cloudea.Domain.Forum.Repositories;

namespace Cloudea.Application.Forum.Events
{
    public class PostLikeDeletedDomainEventHandler
        : IDomainEventHandler<PostLikeDeletedDomainEvent>
    {
        private readonly IForumPostRepository _postRepository;

        public PostLikeDeletedDomainEventHandler(IForumPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task Handle(PostLikeDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(notification.PostId, cancellationToken);
            if (post is null)
            {
                return;
            }

            post.DecreaseLikeCount();
            _postRepository.Update(post);
        }
    }
}
