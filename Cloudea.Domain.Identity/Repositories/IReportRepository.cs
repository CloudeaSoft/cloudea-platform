using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Domain.Identity.Repositories
{
    public interface IReportRepository
    {
        void Add(Report report);

        void Update(Report report);

        void Delete(Report report);

        Task<PageResponse<Report>> ListByUserIdPageRequestAsync(Guid userId, PageRequest request, CancellationToken cancellationToken = default);
    }
}
