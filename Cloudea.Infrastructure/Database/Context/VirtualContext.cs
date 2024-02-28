using Cloudea.Infrastructure.Database;
using Cloudea.Infrastructure.Repositories;
using FreeSql;
using IUnitOfWork = FreeSql.IUnitOfWork;

namespace Cloudea
{
    public class VirtualContext : IDisposable
    {
        public DbContext Context;
        private bool _isInherit;
        private AsyncLocal<string> _transactionId;

        public bool Commited { get; set; }
        private VirtualContext _parent = null;


        private bool _parentSubmited
        {
            get
            {
                if (_parent == null)
                {
                    return false;
                }

                return _parent.Commited;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="isInherit">是否是被嵌套的 被嵌套的不实现SaveChanges 和Dispose</param>
        public VirtualContext(DbContext context, bool isInherit, AsyncLocal<string> transactionId, VirtualContext parent = null)
        {
            Context = context;
            _isInherit = isInherit;
            _transactionId = transactionId;
            if (isInherit)
            {
                _parent = parent;
            }
        }


        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (_isInherit)
            {
                if (_parentSubmited)
                {
                    // 如果父级已经提交了 那这个事务就当作是新的一个事务了
                    int submitRes = await Context.SaveChangesAsync();
                    _transactionId.Value = null;
                    return submitRes;
                }
                return 0;
            }
            int res = await Context.SaveChangesAsync();
            releaseTransactionId();
            return res;
        }
        public virtual int SaveChanges()
        {
            // 是被嵌套的
            if (_isInherit)
            {
                if (_parentSubmited)
                {
                    // 如果父级已经提交了 那这个事务就当作是新的一个事务了
                    int submitRes = Context.SaveChanges();
                    _transactionId.Value = null;
                    return submitRes;
                }
                return 0;
            }

            int res = Context.SaveChanges();
            releaseTransactionId();
            return res;
        }


        public IFreeSql Orm => Context.Orm;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return Context.UnitOfWork;
            }
            set
            {
                Context.UnitOfWork = value;
            }
        }


        private void releaseTransactionId()
        {
            TransactionControlPool.Instance.Remove(_transactionId.Value);
            _transactionId.Value = null;
            Commited = true;
        }

        public void Dispose()
        {

            if (_isInherit && _parentSubmited == false)
            {
                return;
            }
            if (string.IsNullOrEmpty(_transactionId.Value) == false)
            {
                releaseTransactionId();
            }
            Context.Dispose();
        }
    }
}
