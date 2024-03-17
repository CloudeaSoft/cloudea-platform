using Cloudea.Domain.File.Entities;
using Cloudea.Domain.File.Repositories;

namespace Cloudea.Persistence.Repositories.File
{
    public class FileRepository : IFSRepository
    {
        private readonly ApplicationDbContext _context;

        public FileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<File_UploadedFile?> FindFileAsync(long fileSize, string sha256Hash)
        {
            throw new NotImplementedException();
            return null;
        }
    }
}
