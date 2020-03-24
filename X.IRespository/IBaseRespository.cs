using SqlSugar;
using System;
using System.Collections.Generic;

namespace X.IRespository
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseRespository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 新增方法
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>bool</returns>
        bool Add(TEntity entity);
        /// <summary>
        /// 批量新增方法
        /// </summary>
        /// <param name="eitentitysity"></param>
        bool Add(List<TEntity> entitys);

        int AddReturnId(TEntity entitys);
        /// <summary>
        /// 以主键为条件更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Update(TEntity entity);
        /// <summary>
        /// 以主键为条件更新某些列
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        bool Update(System.Linq.Expressions.Expression<Func<TEntity, TEntity>> columns, System.Linq.Expressions.Expression<Func<TEntity, bool>> where);
        /// <summary>
        /// 以主键为条件更新某些列
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="columns">需要更新的列</param>
        /// <returns></returns>
        bool Update(TEntity entity, System.Linq.Expressions.Expression<Func<TEntity, object>> columns);
        /// <summary>
        /// 以非主键为更新条件，更新所有列
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="columns">条件：it => new { it.XId }，单列可以用 it=>it.XId</param>
        /// <returns></returns>
        bool UpdateWhere(TEntity entity, System.Linq.Expressions.Expression<Func<TEntity, object>> columns);
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        int UpdateRange(List<TEntity> list);
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        int AddRange(List<TEntity> list);
        /// <summary>
        /// 根据条件删除-物理删除
        /// </summary>
        /// <param name="whereExp"></param>
        /// <returns></returns>
        int Delete(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereExp);
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        List<TEntity> ToList();
        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(dynamic id);
        /// <summary>
        /// 查询是否存在某记录
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        bool IsExists(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereExpression);
        /// <summary>
        /// 统计查询-Count
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        int Count(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereExpression);
        /// <summary>
        /// 简单查询操作
        /// </summary>
        /// <param name="whereExp"></param>
        /// <returns></returns>
        ISugarQueryable<TEntity> Where(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereExp);
        /// <summary>
        /// 排序查询
        /// </summary>
        /// <param name="whereExp"></param>
        /// <param name="orderexp"></param>
        /// <param name="otype"></param>
        /// <param name="isWithCache"></param>
        /// <param name="cacheDurationSeconds"></param>
        /// <returns></returns>
        ISugarQueryable<TEntity> Where(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereExp, System.Linq.Expressions.Expression<Func<TEntity, object>> orderexp, OrderByType otype = OrderByType.Asc);
        /// <summary>
        /// 简单分页查询
        /// 分页数据缓存，如果开启缓存，则默认最多缓存两页数据
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="page"></param>
        /// <param name="isWithCache">是否缓存</param>
        /// <param name="cacheDurationSeconds"></param>
        /// <returns></returns>
        List<TEntity> Where(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereExpression, PageModel page);
        /// <summary>
        /// 简单排序分页查询
        /// 分页数据缓存，如果开启缓存，则默认最多缓存两页数据
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="page"></param>
        /// <param name="orderexp"></param>
        /// <param name="otype"></param>
        /// <param name="isWithCache"></param>
        /// <param name="cacheDurationSeconds"></param>
        /// <returns></returns>
        List<TEntity> Where(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereExpression, PageModel page, System.Linq.Expressions.Expression<Func<TEntity, object>> orderexp, OrderByType otype = OrderByType.Asc);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="orderexp"></param>
        /// <param name="otype"></param>
        /// <returns></returns>
        ISugarQueryable<TEntity> OrderBy(System.Linq.Expressions.Expression<Func<TEntity, object>> orderexp, OrderByType otype = OrderByType.Asc);

        /// <summary>
        /// 单条查询-超过一条返回异常，无数据返回null
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        TEntity GetSingle(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereExpression);
        /// <summary>
        /// 单条查询-返回第一条，无数据返回null
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        TEntity GetFirst(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereExpression);
        TResult Sum<TResult>(System.Linq.Expressions.Expression<Func<TEntity, TResult>> whereExpression);
    }
}
