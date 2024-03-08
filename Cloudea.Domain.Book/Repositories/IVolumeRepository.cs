using Cloudea.Domain.Book.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Book.Domain.Abstractions
{
    public interface IVolumeRepository
    {
        Task<BookVolume> GetByTitle(string title);
    }
}
