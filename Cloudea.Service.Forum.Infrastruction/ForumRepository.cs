using Cloudea.Entity.Forum;
using Cloudea.Infrastructure.Models;
using Cloudea.Service.Forum.Domain.Repositories;

namespace Cloudea.Service.Forum.Infrastruction
{
    public class ForumRepository : IForumRepository {
        private readonly IFreeSql _database;

        public ForumRepository(IFreeSql database) {
            _database = database;
        }

        public async Task<Result<Forum_Topic>> SaveTopicAsync(Forum_Topic topic) {
            Forum_Topic res = default!;
            using (var uow = _database.CreateUnitOfWork()) {
                try {
                    var topicRepo = _database.GetRepository<Forum_Topic>();
                    var sectionRepo = _database.GetRepository<Forum_Section>();
                    topicRepo.UnitOfWork = uow;
                    sectionRepo.UnitOfWork = uow;

                    res = await topicRepo.InsertAsync(topic);
                    Forum_Section section = await sectionRepo.Select.Where(x => x.Id == topic.SectionId).FirstAsync();
                    section.IncreaseTopicCount();
                    await sectionRepo.UpdateAsync(section);
                    uow.Commit();
                }
                catch (Exception ex) {
                    // IUnitOfWork的Dispose方法包含了Rollback动作，故无需在此调用Rollback
                    return new Error("创建失败");
                }
            }
            return Result<Forum_Topic>.Success(res);
        }
    }
}
