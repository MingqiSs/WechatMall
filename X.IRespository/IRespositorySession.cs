using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using WM.Infrastructure.Extensions.AutofacManager;

namespace X.IRespository
{
    public partial interface IRespositorySession: IDependency
    {
        /// <summary>
        /// 开放所有Sugar操作对象
        /// </summary>
        SqlSugarClient DB {get;}

        #region 2.0 执行SQL
        /// <summary>
        /// 执行sql语句,返回list
        /// 可用于执行存储过程，CommandType.Text方式 ："exec spName @p1"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        List<T> Sql_Query<T>(string sql, params SugarParameter[] parameters) where T : class, new();
        /// <summary>
        ///执行分页查询,返回list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
       // List<T> SqlQueryWithPage<T>(MT.Infrastructure.Models.PageModel page, ref int totalCount) where T : class, new();
        /// <summary>
        /// 执行SQL,返回首行首列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        object Sql_GetScalar(string sql, params SugarParameter[] parameters);
        /// <summary>
        /// 执行SQL，返回受影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int Sql_ExecuteCommand(string sql, params SugarParameter[] parameters);
        /// <summary>
        /// 执行SQL,返回一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        T Sql_QuerySingle<T>(string sql, params SugarParameter[] parameters);
        /// <summary>
        /// 查询返回动态类型
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        dynamic Sql_QueryDynamic(string sql, params SugarParameter[] parameters);
        #endregion

        #region 3.0 存储过程
        /// <summary>
        /// 执行存储过程 CommandType.StoredProcedure 方式
        /// </summary>
        /// <param name="proName"></param>
        /// <param name="parameters">new SugarParameter("@p2", null, true);//isOutput=true</param>
        /// <returns>DataTable</returns>
        DataTable Sql_StoredProcedure(string proName, params SugarParameter[] parameters);
        /// <summary>
        /// 执行存储过程 CommandType.StoredProcedure 方式
        /// </summary>
        /// <param name="proName"></param>
        /// <param name="parameters">new SugarParameter("@p2", null, true);//isOutput=true</param>
        /// <returns>DataTable</returns>
        List<T> Sql_StoredProcedure<T>(string proName, params SugarParameter[] parameters);
        #endregion

        #region 4.0 事务操作
        /// <summary>
        /// 执行事务，返回执行结果
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        DbResult<bool> Sql_Trans(Action action);
        /// <summary>
        /// 执行事务，返回内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        DbResult<T> Sql_Trans<T>(Func<T> func);
        /// <summary>
        /// 执行事务，返回执行结果
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        bool Sql_TransT(Action action);
        #endregion
    }
}
