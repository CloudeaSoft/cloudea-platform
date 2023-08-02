using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Infrastructure.Db
{
    /// <summary>
    /// 基础实体
    /// </summary>
    public class EntityBase
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public long Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column(DbType = "datetime", CanUpdate = false)]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column(DbType = "datetime")]
        public DateTime? UpdateTime { get; set; }
    }
}
