using Cloudea.Service.Book.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Book.Domain.Abstractions
{
    public interface IMetaRepository
    {
        Task<BookMeta> FindMetaByName(string bookName);
        Task<BookMeta> FindMetaById(Guid id);
    }
}
