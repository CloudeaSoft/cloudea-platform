using Cloudea.Entity.Forum;
using Cloudea.Infrastructure.Database;
using Cloudea.Infrastructure.Models;
using Cloudea.Service.Forum.Domain.Repositories;

namespace Cloudea.Service.Forum.Domain
{
    public class ForumSectionRepository(IFreeSql database) : BaseRepository<Forum_Section>(database), IForumSectionRepository {
        public new async Task<Result<Forum_Section>> Read(Guid sectionId) {
            return await base.Read(sectionId);
        }

        public async Task<Result<PageResponse<Forum_Section>>> List(PageRequest request) {
            return await base.GetBaseList(request);
        }

        public async Task<Result<long>> Save(Forum_Section newSection) {
            return await Create(newSection);
        }

        public Task<Result> IncreaseTopicCount(Guid id) {
            throw new NotImplementedException();
        }

        public Task<Result> DecreaseTopicCount(Guid id) {
            throw new NotImplementedException();
        }

        public Task<Result<List<Forum_Section>>> List(List<Guid> idList) {
            throw new NotImplementedException();
        }

        public Task<Result<List<Forum_Section>>> Update(List<Forum_Section> newSection) {
            throw new NotImplementedException();
        }
    }
}
