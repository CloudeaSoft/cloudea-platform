using Cloudea.Application.Abstractions.Messaging;
using Cloudea.Service.Forum.Domain.DomainEvents;
using Cloudea.Service.Forum.Domain.Repositories;

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
            Console.WriteLine(section);
            Console.WriteLine($"========发生了领域事件（{notification.Id}）：一个新的主题帖已被创建，编号为{notification.PostId}，其归属的主题编号为{notification.SectionId}");
        }
    }
}
