using Cloudea.Domain.Book.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Domain.Book.Repositories
{
    public interface IVolumeRepository
    {
        Task<BookVolume> GetByTitle(string title);
    }
}
