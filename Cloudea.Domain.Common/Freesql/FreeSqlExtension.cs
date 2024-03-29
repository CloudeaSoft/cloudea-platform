﻿using Cloudea.Domain.Common.Database;
using Cloudea.Domain.Common.Shared;
using FreeSql;
using FreeSql.Internal.Model;

namespace Cloudea.Domain.Common.Freesql
{
    public static class FreeSqlExtendsion
    {
        /// <summary>
        /// 同步表结构
        /// </summary>
        /// <typeparam name="TOption"></typeparam>
        /// <param name="fsql"></param>
        /// <param name="option"></param>
        public static void SyncStructure(this IFreeSql fsql, IEnumerable<Type> types)
        {
            fsql.CodeFirst.SyncStructure(types.ToArray());
        }

        /// <summary>
        /// 通过sql查找条数( "select count(*) from xxx" )
        /// </summary>
        /// <param name="that"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int FindCountBySql(this IFreeSql that, string sql, object param = null)
        {
            var table = that.Ado.ExecuteDataTable(sql, param);
            object rawCount = table.Rows[0][0];
            if (int.TryParse(rawCount.ToString(), out int count)) {
                return count;
            }
            throw new Exception("parse count error!");
        }

        /// <summary>
        ///  通过sql查找条数( "select count(*) from xxx" )
        /// </summary>
        /// <param name="that"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static async Task<int> FindCountBySqlAsync(this IFreeSql that, string sql, object param = null)
        {
            var table = await that.Ado.ExecuteDataTableAsync(sql, param);
            object rawCount = table.Rows[0][0];
            if (int.TryParse(rawCount.ToString(), out int count)) {
                return count;
            }
            throw new Exception("parse count error!");
        }

        /// <summary>
        /// 获得分页数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="select"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<PageResponse<T>> ToPageList<T>(this ISelect<T> select, int page, int pageSize)
        {
            if (page <= 0) {
                var total = await select.CountAsync();
                var data = await select.ToListAsync();

                return new PageResponse<T>() {
                    Rows = data,
                    Total = total
                };
            }
            else {
                dynamic s = select;
                if (s._orderby == null) {
                    var tables = s._tables as List<SelectTableInfo>;
                    var pktb = tables.Where(a => a.Table.Primarys.Any()).FirstOrDefault();
                    if (pktb != null) {
                        select = select.OrderByPropertyName(pktb?.Table.Primarys.First().Attribute.Name);
                    }
                    else {
                        select = select.OrderByPropertyName(tables.First().Table.Columns.First().Value.Attribute.Name);
                    }
                }
                var list = await select.Count(out long total).Page(page, pageSize).ToListAsync();
                return new PageResponse<T>() {
                    Total = total,
                    Rows = list
                };
            }
        }

        /// <summary>
        /// 获得分页数据 异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="select"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<PageResponse<T>> ToPageListAsync<T>(this ISelect<T> select, PageRequest request)
        {
            return await select.ToPageList(request.PageIndex, request.PageSize);
        }

        /// <summary>
        /// 通过Id查找 异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="select"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<T> FindByIdAsync<T>(this ISelect<T> select, Guid id) where T : BaseDataEntity
        {
            return await select.Where(t => t.Id == id).FirstAsync();
        }

        /// <summary>
        /// 通过sql查询列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fsql"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<T> FindListBySql<T>(this IFreeSql fsql, string sql, object param = null) where T : class
        {
            return fsql.Ado.Query<T>(sql, param);
        }

        /// <summary>
        /// 通过sql查询列表 异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fsql"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<List<T>> FindListBySqlAsync<T>(this IFreeSql fsql, string sql, object param = null) where T : class
        {
            return await fsql.Ado.QueryAsync<T>(sql, param);
        }

        /// <summary>
        /// 通过sql查询单个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fsql"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T FindEntityBySql<T>(this IFreeSql fsql, string sql, object param = null) where T : class
        {
            return fsql.Ado.QuerySingle<T>(sql, param);
        }

        /// <summary>
        /// 通过sql查询单个实体 异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fsql"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<T> FindEntityBySqlAsync<T>(this IFreeSql fsql, string sql, object param = null) where T : class
        {
            return await fsql.Ado.QuerySingleAsync<T>(sql, param);
        }
    }
}