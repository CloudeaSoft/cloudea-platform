namespace Cloudea.Infrastructure.Db;

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}