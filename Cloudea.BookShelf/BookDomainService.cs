using Cloudea.Infrastructure.Models;
using Cloudea.Service.Book.Domain.Abstractions;
using Cloudea.Service.Book.Domain.Entities;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Book.Domain
{
    public class BookDomainService
    {
        private readonly IMetaRepository _metaRepository;
        private readonly IVolumeRepository _volumeRepository;

        public BookDomainService(IMetaRepository metaRepository)
        {
            _metaRepository = metaRepository;
        }

        /// <summary>
        /// 创建书籍信息
        /// </summary>
        public async Task<Result<BookMeta>> CreateMetaAsync(Guid creatorId, string title, string author)
        {
            //查重
            var res = await _metaRepository.FindMetaByName(title);
            if (res is not null) {
                return Result.Fail("书籍已存在");
            }

            Guid metaId = Guid.NewGuid();
            var meta = BookMeta.QuickCreate(metaId, title, author, creatorId);
            return Result.Success(meta);
        }

        /// <summary>
        /// 删除书籍信息
        /// </summary>
        /// <param name="id"></param>
        public async Task<Result<BookMeta>> DeleteMetaAsync(Guid bookId, Guid creator)
        {
            var res = await _metaRepository.FindMetaById(bookId);
            if (res is null) {
                return Result.Fail();
            }
            if (res.Creator != creator) {
                return Result.Fail();
            }
            res.SoftDelete();
            return Result.Success(res);
        }

        public async Task<Result<BookVolume>> UploadVolumeAsync(Guid creatorId, Guid metaId, string title)
        {
            await _volumeRepository.GetByTitle("Title");

            return Result.Success();
        }
    }
}
