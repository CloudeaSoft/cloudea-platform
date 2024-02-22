using Cloudea.Infrastructure.Database;
using FreeSql.DataAnnotations;
using MediatR;
using System.Text.Json.Serialization;

namespace Cloudea.Infrastructure.Domain
{
    public record BaseDomainEntity : BaseDataEntity, IDomainEvent, IHasTimeProperty
    {
        /// <summary>
        /// 领域事件
        /// </summary>
        [JsonIgnore]
        private readonly List<INotification> _domainEvents = [];

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
