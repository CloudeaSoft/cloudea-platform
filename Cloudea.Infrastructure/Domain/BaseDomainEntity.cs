using FreeSql.DataAnnotations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cloudea.Infrastructure.Domain
{
    public record BaseDomainEntity : IDomainEntity, IDomainEvents, IHasTimeProperty
    {
        /// <summary>
        /// 领域事件
        /// </summary>
        [JsonIgnore]
        private readonly List<INotification> _domainEvents = [];

        /// <summary>
        /// 物理主键
        /// </summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public long AutoIncId { get; set; }

        /// <summary>
        /// 逻辑主键
        /// </summary>
        [Column()]
        public Guid Id { get; protected set; } = Guid.NewGuid();

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column(DbType = "timestamp", CanUpdate = false)]
        public DateTime CreationTime { get; private set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [Column(DbType = "timestamp")]
        public DateTime ModificationTime { get; private set; }

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        public void AddDomainEventIfAbsent(INotification eventItem)
        {
            if (!_domainEvents.Contains(eventItem)) {
                _domainEvents.Add(eventItem);
            }
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public IEnumerable<INotification> GetDomainEvents()
        {
            return _domainEvents;
        }
    }
}
