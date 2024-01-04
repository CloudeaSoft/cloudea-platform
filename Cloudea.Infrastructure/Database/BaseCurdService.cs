using Cloudea;
using Cloudea.Infrastructure.Domain;
using Cloudea.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cloudea.Infrastructure.Database
{
    public abstract class BaseCurdService<TEntity>
        where TEntity : BaseDataEntity, new()
    {
        public IFreeSql _database { get; set; }

        protected BaseCurdService(IFreeSql database)
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
            if (!CheckModel(entity, out string errMsg)) {
                return Result.Fail(errMsg);
            }
            long id = await _database.Insert(entity).ExecuteIdentityAsync();
            return Result.Success(id);
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<Result<long>> Create(List<TEntity> list)
        {
            long id = await _database.Insert(list).ExecuteIdentityAsync();
            return Result.Success(id);
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
            return Result<List<TEntity>>.Success(entityList);
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
                return Result.Fail("Id不存在");
            }
            return Result<TEntity>.Success(entity);
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
        public virtual bool CheckModel(TEntity entity, out string message)
        {
            if (entity == null) {
                message = "数据不能为空";
                return false;
            }
            message = "";
            return true;
        }

        /// <summary>
        /// 获得基础列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public virtual async Task<Result<ResponsePage<TEntity>>> GetBaseList(RequestPage req)
        {
            return Result.Success(await _database.Select<TEntity>().ToPageListAsync(req));
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
        public virtual async Task<Result> Update(TEntity entity)
        {
            if (!CheckModel(entity, out string errMsg)) {
                return Result.Fail(errMsg);
            }
            await _database.Update<TEntity>().SetSource(entity).ExecuteAffrowsAsync();
            return Result.Success();
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<Result> Delete(Guid id)
        {
            if (await _database.Select<TEntity>().AnyAsync(t => t.Id == id)) {
                await _database.Select<TEntity>().Where(t => t.Id == id).ToDelete().ExecuteAffrowsAsync();
                return Result.Success();
            }
            else {
                return Result.Fail("数据不存在");
            }
        }

        /// <summary>
        /// 批量数据列表
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public virtual async Task<Result> DeleteList(List<Guid> idList)
        {
            // 合法性检查
            if (idList == null || idList.Count == 0) {
                return Result.Fail("删除数据不能为空");
            }
            await _database.Select<TEntity>().Where(t => idList.Contains(t.Id)).ToDelete().ExecuteAffrowsAsync();
            return Result.Success();
        }
        #endregion
    }
}