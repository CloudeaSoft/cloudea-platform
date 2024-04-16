using Cloudea.Application.Abstractions.Messaging;
using Cloudea.Domain.Forum.DomainEvents;
using Cloudea.Domain.Forum.Repositories;

namespace Cloudea.Application.Forum.Events
{
    public class PostFavoriteCreatedDomainEventHandler
        : IDomainEventHandler<PostFavoriteCreatedDomainEvent>
    {
        private readonly IForumPostRepository _postRepository;

        public PostFavoriteCreatedDomainEventHandler(IForumPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task Handle(PostFavoriteCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(notification.PostId, cancellationToken);
            if (post is null)
            {
                return;
            }

            post.IncreaseFavoriteCount();
            _postRepository.Update(post);
        }
    }
}
