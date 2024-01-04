using Cloudea.Infrastructure.Database;
using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Book.Domain.Entities;

[AutoGenerateTable]
[Table(Name = "book_content")]
public record BookContent : BaseDataEntity
{
    [Column(DbType = "varchar(10000)")]
    public string Content { get; set; }
}
