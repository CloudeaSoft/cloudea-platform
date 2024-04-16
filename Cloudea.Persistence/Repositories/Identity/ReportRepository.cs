using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Identity.Entities;
using Cloudea.Domain.Identity.Repositories;

namespace Cloudea.Persistence.Repositories.Identity
{
    internal class ReportRepository : IReportRepository
    {
        public void Add(Report report)
        {
            throw new NotImplementedException();
        }

        public void Delete(Report report)
        {
            throw new NotImplementedException();
        }

        public Task<PageResponse<Report>> ListByUserIdPageRequestAsync(Guid userId, PageRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Update(Report report)
        {
            throw new NotImplementedException();
        }
    }
}
