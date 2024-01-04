using FreeSql.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cloudea.Infrastructure.Database
{
    public record BaseDataEntity
    {
        /// <summary>
        /// 物理主键
        /// </summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        [JsonIgnore]
        public long AutoIncId { get; set; }

        /// <summary>
        /// 逻辑主键
        /// </summary>
        [Column()]
        public Guid Id { get; protected set; } = Guid.NewGuid();
    }
}
