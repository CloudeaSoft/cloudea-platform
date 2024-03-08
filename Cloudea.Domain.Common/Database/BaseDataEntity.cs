using System.Text.Json.Serialization;

namespace Cloudea.Infrastructure.Database
{
    public abstract class BaseDataEntity
    {
        /// <summary>
        /// 物理主键
        /// </summary>
        [JsonIgnore]
        public long AutoIncId { get; set; }

        /// <summary>
        /// 逻辑主键
        /// </summary>
        public Guid Id { get; protected set; }
    }
}
