using Cloudea.Entity.Forum;
using Cloudea.Infrastructure.Models;
using Cloudea.Service.Forum.Domain.Abstractions;
using Org.BouncyCastle.Asn1.X509.Qualified;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Forum.Domain
{
    public class ForumDomainService
    {
        private readonly IForumTopicRepository _forumTopicRepository;
        private readonly IForumSectionRepository _forumSectionRepository;

        public ForumDomainService(IForumTopicRepository forumTopicRepository, IForumSectionRepository forumSectionRepository)
        {
            _forumTopicRepository = forumTopicRepository;
            _forumSectionRepository = forumSectionRepository;
        }

        public async Task<Result<Forum_Topic>> GetTopicAsync(Guid topicId)
        {
            return await _forumTopicRepository.Get(topicId);
        }

        public async Task<Result<List<Forum_Topic>>> ListTopicAsync()
        {
            return await _forumTopicRepository.List();
        }

        public async Task<Result<long>> PostTopicAsync(
            Guid userId,
            Guid sectionId,
            string title,
            string content)
        {
            var newTopic = Forum_Topic.Create(userId, sectionId, title, content);

            return await _forumTopicRepository.SaveTopic(newTopic);
        }

        public async Task<Result<List<Forum_Section>>> ListSectionAsync()
        {
            return await _forumSectionRepository.List();
        }

        public async Task<Result<long>> CreateSectionAsync(string name, Guid masterId)
        {
            var newSection = Forum_Section.Create(name, masterId);

            return await _forumSectionRepository.SaveSection(newSection);
        }
    }
}
