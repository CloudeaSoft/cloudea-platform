using Cloudea.Domain.Common.Freesql.Context;

namespace Cloudea.Domain.Common.Freesql
{
    public static class FreeSqlTransactionExtendsion
    {
        /// <summary>
        /// 事务
        /// </summary>
        /// <param name="that"></param>
        /// <returns></returns>
        public static VirtualContext BeginTransaction<TOption>(this IFreeSql<TOption> that)
        {
            if (string.IsNullOrWhiteSpace(TransactionControl<TOption>.TransactionId.Value)) {
                // 是全新的事务
                var context = that.CreateDbContext();

                var virtualContext = new VirtualContext(context, false, TransactionControl<TOption>.TransactionId);

                TransactionControl<TOption>.TransactionId.Value = Guid.NewGuid().ToString();
                TransactionControlPool.Instance.SetContext(TransactionControl<TOption>.TransactionId.Value, virtualContext);

                return virtualContext;
            }
            else {
                var existContext = TransactionControlPool.Instance.GetContext(TransactionControl<TOption>.TransactionId.Value);

                if (existContext == null) {
                    var dbContext = that.CreateDbContext();
                    var virutalContext = new VirtualContext(dbContext, false, TransactionControl<TOption>.TransactionId);
                    TransactionControl<TOption>.TransactionId.Value = Guid.NewGuid().ToString();
                    TransactionControlPool.Instance.SetContext(TransactionControl<TOption>.TransactionId.Value, virutalContext);
                    return virutalContext;
                }

                return new VirtualContext(existContext.Context, true, TransactionControl<TOption>.TransactionId, existContext);
            }
        }

        internal static Func<IFreeSql, VirtualContext> GetDefaultTransaction = null;

        /// <summary>
        /// 事务
        /// </summary>
        /// <param name="that"></param>
        /// <returns></returns>
        public static VirtualContext BeginTransaction(this IFreeSql that)
        {
            if (GetDefaultTransaction == null) {
                throw new NotImplementedException("IFreeSql 未设置默认");
            }
            return GetDefaultTransaction(that);
        }
    }
}
