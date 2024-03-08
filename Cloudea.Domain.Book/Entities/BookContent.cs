using Cloudea.Infrastructure.Database;

namespace Cloudea.Service.Book.Domain.Entities;

public class BookContent : BaseDataEntity
{
    public string Content { get; set; }
}
