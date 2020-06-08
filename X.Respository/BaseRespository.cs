using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.IRespository;


namespace X.Respository
{
    public class BaseRespository<TEntity> : IBaseRespository<TEntity>
        where TEntity : class, new()
    {
        #region 1.0 实现单例的SqlSugarClient上下文
        public virtual SqlSugarClient db
        {
            get
            {
                return DBOperation.GetClient_WMDB();
            }
        }
        #endregion

        #region 2.0 封装dbSet
        SimpleClient<TEntity> dbSet;
        public SimpleClient<TEntity> DbSet
        {
            get
            {
                return dbSet;
            }
        }

        public BaseRespository()
        {
            dbSet = new SimpleClient<TEntity>(db);
        }
        #endregion

        #region 3.0 主要数据操作
        /// <summary>
        /// 新增方法
        /// </summary>
        /// <param name="eitity"></param>
        public bool Add(TEntity entity)
        {
            return db.Insertable(entity).ExecuteCommand() > 0;
        }
        /// <summary>
        /// 新增方法
        /// </summary>
        /// <param name="eitity"></param>
        public async Task<bool> AddAsync(TEntity entity)
        {
            return await db.Insertable(entity).ExecuteCommandAsync() > 0;
        }
        /// <summary>
        /// 批量新增方法
        /// </summary>
        /// <param name="eitentitysity"></param>
        public bool Add(List<TEntity> entitys)
        {
            return db.Insertable(entitys).ExecuteCommand() > 0;
        }
        /// <summary>
        /// 新增返回自增ID
        /// </summary>
        /// <param name="eitity"></param>
        public int AddReturnId(TEntity entitys)
        {
            return db.Insertable(entitys).ExecuteReturnIdentity();
        }
        /// <summary>
        /// 以主键为条件更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(TEntity entity)
        {
            return db.Updateable(entity).ExecuteCommand() > 0;
        }
        /// <summary>
        /// 以主键为条件更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(TEntity entity)
        {
            return await db.Updateable(entity).ExecuteCommandAsync() > 0;
        }
        /// <summary>
        /// 以主键为条件更新某些列
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="columns">需要更新的列:it => new { it.Name }</param>
        /// <returns></returns>
        public bool Update(System.Linq.Expressions.Expression<Func<TEntity, TEntity>> columns, System.Linq.Expressions.Expression<Func<TEntity, bool>> whereExp)
        {
            return db.Updateable<TEntity>().UpdateColumns(columns).Where(whereExp).ExecuteCommand() > 0;
        }

        /// <summary>
        /// 以主键为条件更新某些列
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="columns">需要更新的列:it => new { it.Name }</param>
        /// <returns></returns>
        public bool Update(TEntity entity, System.Linq.Expressions.Expression<Func<TEntity, object>> columns)
        {
            return db.Updateable(entity).UpdateColumns(columns).ExecuteCommand() > 0;
        }

        /// <summary>
        /// 以非主键为更新条件，更新所有列
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="columns">条件：it => new { it.XId }，单列可以用 it=>it.XId</param>
        /// <returns></returns>
        public bool UpdateWhere(TEntity entity, System.Linq.Expressions.Expression<Func<TEntity, object>> columns)
        {
            return db.Updateable(entity).WhereColumns(columns).ExecuteCommand() > 0;
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int UpdateRange(List<TEntity> list)
        {
            if (list.Count == 0) return 0;
            return db.Updateable(list).ExecuteCommand();
        }
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int AddRange(List<TEntity> list)
        {
            if (list.Count == 0) return 0;
            return db.Insertable(list).ExecuteCommand();
        }

        /// <summary>
        /// 根据条件表达式删除
        /// </summary>
        /// <param name="whereExp"></param>
        /// <returns></returns>
        public int Delete(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereExp)
        {
            return db.Deleteable<TEntity>().Where(whereExp).ExecuteCommand();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public List<TEntity> ToList()
        {
            return db.Queryable<TEntity>().ToList();
        }

        /// <summary>
        /// 简单查询操作
        /// </summary>
        /// <param name="whereExp"></param>
        /// <returns></returns>
        public ISugarQueryable<TEntity> Where(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereExp)
        {
            return db.Queryable<TEntity>().Where(whereExp);
        }

        /// <summary>
        /// 排序查询
        /// </summary>
        /// <param name="whereExp"></param>
        /// <param name="orderexp"></param>
        /// <param name="otype"></param>
        /// <returns></returns>
        public ISugarQueryable<TEntity> Where(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereExp, System.Linq.Expressions.Expression<Func<TEntity, object>> orderexp, OrderByType otype = OrderByType.Asc)
        {
            return db.Queryable<TEntity>().Where(whereExp).OrderBy(orderexp, otype);
        }

        /// <summary>
        /// 简单分页查询
        /// 分页数据缓存，如果开启缓存，则默认最多缓存两页数据
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<TEntity> Where(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereExpression, PageModel page)
        {
            int totalcount = 0;
            var result = db.Queryable<TEntity>().Where(whereExpression).ToPageList(page.PageIndex, page.PageSize, ref totalcount);
            page.PageCount = totalcount;
            return result;
        }

        /// <summary>
        /// 简单排序分页查询
        /// 分页数据缓存，如果开启缓存，则默认最多缓存两页数据
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="page"></param>
        /// <param name="orderexp"></param>
        /// <param name="otype"></param>
        /// <returns></returns>
        public List<TEntity> Where(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereExpression, PageModel page, System.Linq.Expressions.Expression<Func<TEntity, object>> orderexp, OrderByType otype = OrderByType.Asc)
        {
            int totalcount = 0;
            var result = db.Queryable<TEntity>().Where(whereExpression).OrderBy(orderexp, otype).ToPageList(page.PageIndex, page.PageSize, ref totalcount);
            page.PageCount = totalcount;
            return result;
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="orderexp"></param>
        /// <param name="otype"></param>
        /// <returns></returns>
        public ISugarQueryable<TEntity> OrderBy(System.Linq.Expressions.Expression<Func<TEntity, object>> orderexp, OrderByType otype = OrderByType.Asc)
        {
            return db.Queryable<TEntity>().OrderBy(orderexp, otype);
        }

        #endregion

        #region 4.0 Tools
        /// <summary>
        /// 根据ID获取数据
        /// ID为主键
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetById(dynamic id)
        {
            return DbSet.GetById(id);
        }

        /// <summary>
        /// 查询是否存在某记录
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public bool IsExists(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereExpression)
        {
            return DbSet.IsAny(whereExpression);
        }

        /// <summary>
        /// 统计查询-Count
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public int Count(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereExpression)
        {
            return DbSet.Count(whereExpression);
        }

        /// <summary>
        /// 统计查询-Count
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public TResult Sum<TResult>(System.Linq.Expressions.Expression<Func<TEntity, TResult>> whereExpression)
        {
            return db.Queryable<TEntity>().Sum(whereExpression);
        }

        /// <summary>
        /// 单条查询-超过一条返回异常，无数据返回null
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public TEntity GetSingle(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereExpression)
        {
            return db.Queryable<TEntity>().Single(whereExpression);
        }

        /// <summary>
        /// 单条查询-返回第一条，无数据返回null
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public TEntity GetFirst(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereExpression)
        {
            return db.Queryable<TEntity>().First(whereExpression);
        }

        #endregion



    }
}
