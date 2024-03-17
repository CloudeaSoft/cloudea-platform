using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Domain.Common.Freesql.Context
{
    /// <summary>
    /// 事务管理池
    /// </summary>
    public class TransactionControlPool
    {
        public static TransactionControlPool Instance { get; private set; } = new TransactionControlPool();
        private TransactionControlPool() { }

        private ConcurrentDictionary<string, VirtualContext> _pool = new ConcurrentDictionary<string, VirtualContext>();


        public VirtualContext? GetContext(string transactionId)
        {
            if (_pool.TryGetValue(transactionId, out VirtualContext? dbContext)) {
                return dbContext;
            }
            return null;
        }
        public void SetContext(string transactionId, VirtualContext context)
        {
            _pool.AddOrUpdate(transactionId, context, (a, b) => context);
        }

        public void Remove(string transactionId)
        {
            _pool.Remove(transactionId, out _);
        }
    }

    /// <summary>
    /// 事务传播控制
    /// </summary>
    public class TransactionControl<T>
    {
        /// <summary>
        /// 当前运行中的事务
        /// </summary>
        public static AsyncLocal<string> TransactionId = new AsyncLocal<string>();
    }
}
