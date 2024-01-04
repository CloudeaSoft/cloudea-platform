using Cloudea.Infrastructure.Database;
using Cloudea.Service.Book.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Book.Infrastructure
{
    public class MetaDbContext : BaseCurdService<BookMeta>
    {
        public MetaDbContext(IFreeSql database) : base(database)
        {
            _database = database;
        }
    }
}
