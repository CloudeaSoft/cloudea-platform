using Cloudea.Service.Book.Domain.Abstractions;
using Cloudea.Service.Book.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Book.Infrastructure
{
    public class VolumeRepository : IVolumeRepository
    {
        public Task<BookVolume> GetByTitle(string title)
        {
            throw new NotImplementedException();
        }
    }
}
