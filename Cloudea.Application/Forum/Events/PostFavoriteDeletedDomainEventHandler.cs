using Cloudea.Application.Abstractions.Messaging;
using Cloudea.Domain.Forum.DomainEvents;
using Cloudea.Domain.Forum.Repositories;

namespace Cloudea.Application.Forum.Events
{
    public class PostFavoriteDeletedDomainEventHandler
        : IDomainEventHandler<PostFavoriteDeletedDomainEvent>
    {
        private readonly IForumPostRepository _postRepository;

        public PostFavoriteDeletedDomainEventHandler(IForumPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task Handle(PostFavoriteDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(notification.PostId, cancellationToken);
            if (post is null)
            {
                return;
            }

            post.DecreaseFavoriteCount();
            _postRepository.Update(post);
        }
    }
}
