using Cloudea.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Cloudea.Service.Book.Domain.Entities;
using Cloudea.Infrastructure.Database;
using Cloudea.Service.Book.Domain.Abstractions;

namespace Cloudea.Service.Book.Infrastructure
{
    public class MetaRepository : BaseCurdService<BookMeta>, IMetaRepository
    {
        private readonly ILogger<MetaRepository> logger;

        public MetaRepository(IFreeSql database, ILogger<MetaRepository> logger) : base(database)
        {
            _database = database;
            this.logger = logger;
        }

        public async Task<BookMeta> FindMetaById(Guid id)
        {
            var res = await _database.Select<BookMeta>().Where(e => e.Id == id).FirstAsync();
            return res;
        }

        public async Task<BookMeta> FindMetaByName(string bookName)
        {
            return await _database.Select<BookMeta>().Where(e => e.Title == bookName).FirstAsync();
        }
    }
}