using Cloudea.Domain.Common.Shared;
using Microsoft.EntityFrameworkCore;

namespace Cloudea.Persistence.Extensions
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// 获得分页数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="select"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<PageResponse<T>> ToPageListAsync<T>(this IQueryable<T> source, PageRequest request, CancellationToken cancellationToken = default)
        {
            var count = source.Count();
            if (count <= 0) {
                return new PageResponse<T>();
            }
            var list = await source.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);
            return new PageResponse<T>() {
                Total = count,
                Rows = list ?? []
            };
        }
    }
}
