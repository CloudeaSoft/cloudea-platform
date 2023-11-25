﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Infrastructure.Domain;

public interface IDomainEvents
{
    IEnumerable<INotification> GetDomainEvents();
    void AddDomainEvent(INotification eventItem);
    /// <summary>
    /// 如果已经存在这个元素，则跳过，否则增加。以避免对于同样的事件触发多次（比如在一个事务中修改领域模型的多个对象）
    /// </summary>
    /// <param name="eventItem"></param>
    void AddDomainEventIfAbsent(INotification eventItem);
    public void ClearDomainEvents();
}
