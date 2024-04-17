using Cloudea.Application.Abstractions.Messaging;
using Cloudea.Domain.Forum.DomainEvents;
using Cloudea.Domain.Forum.Repositories;

namespace Cloudea.Application.Forum.Events;

public class PostLikeCreatedDomainEventHandler
    : IDomainEventHandler<PostLikeCreatedDomainEvent>
{
    private readonly IForumPostRepository _postRepository;

    public PostLikeCreatedDomainEventHandler(IForumPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task Handle(PostLikeCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(notification.PostId, cancellationToken);
        if (post is null)
        {
            return;
        }

        post.IncreaseLikeCount();
        _postRepository.Update(post);
    }
}
