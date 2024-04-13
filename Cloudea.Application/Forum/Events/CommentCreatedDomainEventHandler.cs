using Cloudea.Application.Abstractions.Messaging;
using Cloudea.Domain.Forum.DomainEvents;
using Cloudea.Domain.Forum.Repositories;

namespace Cloudea.Application.Forum.Events
{
    public class CommentCreatedDomainEventHandler
        : IDomainEventHandler<CommentCreatedDomainEvent>
    {
        private readonly IForumReplyRepository _forumReplyRepository;

        public CommentCreatedDomainEventHandler(IForumReplyRepository forumReplyRepository)
        {
            _forumReplyRepository = forumReplyRepository;
        }

        public async Task Handle(CommentCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var reply = await _forumReplyRepository.GetByIdAsync(notification.ReplyId, cancellationToken);
            if (reply is null)
            {
                return;
            }
            reply.IncreaseCommentCount();
            _forumReplyRepository.Update(reply);
        }
    }
}
