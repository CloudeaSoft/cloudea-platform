using Cloudea.Domain.Book.Entities;
using Cloudea.Domain.Common.Shared;
using Cloudea.Infrastructure.Shared;
using Cloudea.Service.Book.Domain.Abstractions;
using Cloudea.Service.Book.Domain.Entities;

namespace Cloudea.Service.Book.Domain
{
    public class BookDomainService
    {
        private readonly IMetaRepository _metaRepository;
        private readonly IVolumeRepository _volumeRepository;

        public BookDomainService(IMetaRepository metaRepository, IVolumeRepository volumeRepository)
        {
            _metaRepository = metaRepository;
            _volumeRepository = volumeRepository;
        }

        /// <summary>
        /// 创建书籍信息
        /// </summary>
        public async Task<Result<BookMeta>> CreateMetaAsync(Guid creatorId, string title, string author)
        {
            //查重
            var res = await _metaRepository.FindMetaByName(title);
            if (res is not null) {
                return new Error("书籍已存在");
            }

            Guid metaId = Guid.NewGuid();
            var meta = BookMeta.QuickCreate(metaId, title, author, creatorId);
            return meta;
        }

        /// <summary>
        /// 删除书籍信息
        /// </summary>
        /// <param name="id"></param>
        public async Task<Result<BookMeta>> DeleteMetaAsync(Guid bookId, Guid creator)
        {
            var res = await _metaRepository.FindMetaById(bookId);
            if (res is null) {
                return new Error("");
            }
            if (res.Creator != creator) {
                return new Error("");
            }
            res.SoftDelete();
            return res;
        }

        public async Task<Result<BookVolume>> UploadVolumeAsync(Guid creatorId, Guid metaId, string title)
        {
            var res = await _volumeRepository.GetByTitle("Title");
            throw new NotImplementedException();
            return res;
        }
    }
}
