using Cloudea.Domain.Common.Database;
using Cloudea.Domain.Common.Shared;
using System.Linq.Expressions;

namespace Cloudea.Domain.Common.Freesql
{
    public abstract class BaseRepository<TEntity>
        where TEntity : BaseDataEntity, new()
    {
        protected IFreeSql _database { get; set; }

        protected BaseRepository(IFreeSql database)
        {
            _database = database;
        }

        #region 增加
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<Result<long>> Create(TEntity entity)
        {
            if (!CheckModel(entity, out Error errMsg)) {
                return Result.Failure<long>(errMsg);
            }
            long id = await _database.Insert(entity).ExecuteIdentityAsync();
            return id;
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<Result<long>> Create(List<TEntity> list)
        {
            long id = await _database.Insert(list).ExecuteIdentityAsync();
            return id;
        }
        #endregion

        #region 查询
        /// <summary>
        /// Select
        /// </summary>
        /// <returns></returns>
        public virtual FreeSql.ISelect<TEntity> BaseSelect()
        {
            return _database.Select<TEntity>();
        }

        public virtual async Task<Result<List<TEntity>>> Read()
        {
            var entityList = await BaseSelect().ToListAsync();
            return entityList;
        }

        /// <summary>
        /// 通过 id 查询实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<Result<TEntity>> Read(Guid id)
        {
            var entity = await _database.Select<TEntity>().Where(t => t.Id == id).FirstAsync();
            if (entity == null) {
                return BaseRepositoryErrors.IdNotExist;
            }
            return entity;
        }

        /// <summary>
        /// 通过where 条件找实体
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FindEntityByWhere(Expression<Func<TEntity, bool>> exp)
        {
            return await _database.Select<TEntity>().Where(exp).FirstAsync();
        }

        /// <summary>
        /// 通过where 条件找实体 列表
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> FindListByWhere(Expression<Func<TEntity, bool>> exp)
        {
            return await _database.Select<TEntity>().Where(exp).ToListAsync();
        }

        /// <summary>
        /// 检查模型是否正确
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool CheckModel(TEntity entity, out Error error)
        {
            if (entity == null) {
                error = BaseRepositoryErrors.DataCannotBeEmpty;
                return false;
            }
            error = Error.None;
            return true;
        }

        /// <summary>
        /// 获得基础列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public virtual async Task<Result<PageResponse<TEntity>>> GetBaseList(PageRequest req)
        {
            return await _database.Select<TEntity>().ToPageListAsync(req);
        }

        /// <summary>
        /// 检查 Id是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<bool> Exist(Guid id)
        {
            return await _database.Select<TEntity>().Where(t => t.Id == id).AnyAsync();
        }

        /// <summary>
        /// 检查 给定判断
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public virtual async Task<bool> Exist(Expression<Func<TEntity, bool>> exp)
        {
            return await _database.Select<TEntity>().Where(exp).AnyAsync();
        }
        #endregion

        #region 修改
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<Result<int>> Update(TEntity entity)
        {
            if (!CheckModel(entity, out Error errMsg)) {
                return errMsg;
            }
            var res = await _database.Update<TEntity>().SetSource(entity).ExecuteAffrowsAsync();
            return res;
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<Result<int>> Delete(Guid id)
        {
            if (await _database.Select<TEntity>().AnyAsync(t => t.Id == id)) {
                var res = await _database.Select<TEntity>().Where(t => t.Id == id).ToDelete().ExecuteAffrowsAsync();
                return res;
            }
            else {
                return BaseRepositoryErrors.DataNotExist;
            }
        }

        /// <summary>
        /// 批量数据列表
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public virtual async Task<Result<int>> DeleteList(List<Guid> idList)
        {
            // 合法性检查
            if (idList == null || idList.Count == 0) {
                return BaseRepositoryErrors.DeleteNullData;
            }
            var res = await _database.Select<TEntity>().Where(t => idList.Contains(t.Id)).ToDelete().ExecuteAffrowsAsync();
            return res;
        }
        #endregion
    }

    public static class BaseRepositoryErrors
    {
        public static readonly Error DeleteNullData = new("删除数据不能为空");

        public static readonly Error DataNotExist = new("数据不存在");

        public static readonly Error DataCannotBeEmpty = new("数据不能为空");

        public static readonly Error IdNotExist = new("Id不存在");
    }
}