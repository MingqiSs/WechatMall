using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace X.Respository
{
    public partial class RespositorySession
    {
        #region 1.0 实现单例的SqlSugarClient上下文
        public virtual SqlSugarClient DB
        {
            get
            {
                return DBOperation.GetClient_WMDB();
            }
        }
        #endregion

        #region 2.0 执行SQL
        /// <summary>
        /// 执行sql语句,返回list
        /// 可用于执行存储过程，CommandType.Text方式 ："exec spName @p1"
        /// 不支持output
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<T> Sql_Query<T>(string sql, params SugarParameter[] parameters) where T : class, new()
        {
            return DB.Ado.SqlQuery<T>(sql, parameters);
        }
        public async Task<List<T>> Sql_QueryAsync<T>(string sql, params SugarParameter[] parameters) where T : class, new()
        {
            return await DB.Ado.SqlQueryAsync<T>(sql, parameters);
        }
        /// <summary>
        ///执行分页查询,返回list     
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<T> SqlQueryWithPage<T>(WM.Infrastructure.Models.PageModel page,ref int totalCount)
        {

            string sql = $"Select COUNT(1) From {page.Table} Where {page.Where}";
            totalCount = DB.Ado.SqlQuerySingle<int>(sql);
            if (totalCount > 0)
            {
                sql = $"Select {page.Query} From {page.Table} Where {page.Where} Order BY {page.Order} LIMIT {(page.Pageindex > 0 ? page.Pageindex - 1 : 0) * page.PageSize},{page.PageSize}";
               var list= DB.Ado.SqlQuery<T>(sql);
                return list;
            }
            return null;
        }
        /// <summary>
        /// 执行SQL,返回首行首列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object Sql_GetScalar(string sql, params SugarParameter[] parameters)
        {
            return DB.Ado.GetScalar(sql, parameters);
        }
        /// <summary>
        /// 执行SQL，返回受影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int Sql_ExecuteCommand(string sql, params SugarParameter[] parameters)
        {
            return DB.Ado.ExecuteCommand(sql, parameters);
        }
        /// <summary>
        /// 执行SQL,返回一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public T Sql_QuerySingle<T>(string sql, params SugarParameter[] parameters)
        {
            return DB.Ado.SqlQuerySingle<T>(sql, parameters);
        }
        /// <summary>
        /// 查询返回动态类型
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public dynamic Sql_QueryDynamic(string sql, params SugarParameter[] parameters)
        {
            return DB.Ado.SqlQueryDynamic(sql, parameters);
        }
        #endregion

        #region 3.0 存储过程
        /// <summary>
        /// 执行存储过程 CommandType.StoredProcedure 方式
        /// </summary>
        /// <param name="proName"></param>
        /// <param name="parameters">new SugarParameter("@p2", null, true);//isOutput=true</param>
        /// <returns>DataTable</returns>
        public DataTable Sql_StoredProcedure(string proName, params SugarParameter[] parameters)
        {
            //var p22 = new SugarParameter("@p2", null, true);//isOutput=true
            return DB.Ado.UseStoredProcedure().GetDataTable(proName, parameters);
        }
        /// <summary>
        /// 执行存储过程 CommandType.StoredProcedure 方式
        /// </summary>
        /// <param name="proName"></param>
        /// <param name="parameters">new SugarParameter("@p2", null, true);//isOutput=true</param>
        /// <returns>DataTable</returns>
        public List<T> Sql_StoredProcedure<T>(string proName, params SugarParameter[] parameters)
        {
            //var p22 = new SugarParameter("@p2", null, true);//isOutput=true
            return DB.Ado.UseStoredProcedure().SqlQuery<T>(proName, parameters);
        }
        #endregion

        #region 4.0 事务操作
        /// <summary>
        /// 执行事务，返回执行结果
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public DbResult<bool> Sql_Trans(Action action)
        {
            return DB.Ado.UseTran(action);
        }
        /// <summary>
        /// 执行事务，返回内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public DbResult<T> Sql_Trans<T>(Func<T> func)
        {
            return DB.Ado.UseTran(func);
        }
        /// <summary>
        /// 执行事务，返回执行结果
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool Sql_TransT(Action action)
        {
            try
            {
                DB.Ado.BeginTran();

                //操作
                action();

                DB.Ado.CommitTran();
                return true;
            }
            catch (Exception ex)
            {
                DB.Ado.RollbackTran();
                return false;
            }

        }

        public bool Add<TEntity>(TEntity entities) where TEntity : class, new()
        {
            return DB.Insertable(entities).ExecuteCommand() > 0;
            
        }
        public  bool AddRange<TEntity>(List<TEntity> entities)
        {
            return DB.Insertable(entities).ExecuteCommand() > 0;
        }
        public bool Update<TEntity>(TEntity entities) where TEntity : class, new()
        {
            return DB.Updateable(entities).ExecuteCommand() > 0;

        }
        #endregion

    }
}
