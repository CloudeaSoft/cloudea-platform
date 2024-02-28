using FreeSql.DataAnnotations;
using MediatR;
using System.Text.Json.Serialization;

namespace Cloudea.Infrastructure.Database
{
    public abstract class BaseEntity : BaseDataEntity, IHasTimeProperty
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column(DbType = "datetime", CanUpdate = false)]
        public DateTime CreationTime { get; protected set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [Column(DbType = "datetime")]
        public DateTime ModificationTime { get; protected set; }
    }
}
