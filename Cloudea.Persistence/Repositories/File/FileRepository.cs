using Cloudea.Domain.File.Entities;
using Cloudea.Domain.File.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cloudea.Persistence.Repositories.File
{
    public class FileRepository : IFileRepository
    {
        private readonly ApplicationDbContext _context;

        public FileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(UploadedFile file) =>
            _context.Set<UploadedFile>().Add(file);

        public void Delete(UploadedFile file) =>
            _context.Set<UploadedFile>().Remove(file);

        public async Task<UploadedFile?> GetBySizeHashAsync(
            long fileSize,
            string sha256Hash,
             CancellationToken cancellation = default) =>
            await _context.Set<UploadedFile>()
                .Where(x =>
                    x.FileSizeInBytes == fileSize &&
                    x.FileSHA256Hash == sha256Hash)
                .FirstOrDefaultAsync(cancellation);

        public async Task<UploadedFile?> GetByUriAsync(
            Uri path,
            CancellationToken cancellationToken = default) =>
            await _context.Set<UploadedFile>()
                .Where(x =>
                    x.RemoteUrl == path ||
                    x.BackupUrl == path)
                .FirstOrDefaultAsync();
    }
}
