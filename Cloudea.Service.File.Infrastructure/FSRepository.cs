using Cloudea.Service.File.Domain;
using Cloudea.Service.File.Domain.Entities;

namespace Cloudea.Service.File.Infrastructure;

public class FSRepository : IFSRepository
{
    private readonly IFreeSql _database;

    public FSRepository(IFreeSql database)
    {
        _database = database;
    }

    public async Task<File_UploadedFile?> FindFileAsync(long fileSize, string sha256Hash)
    {
        return null;
    }
}
