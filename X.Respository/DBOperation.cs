using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using WM.Infrastructure.Config;
using X.Common;

namespace X.Respository
{
    public class DBOperation
    {
        static string ConnectString_MasterDB = AppSetting.GetConfig("ConnectionStrings:WMDB");
        public static SqlSugarClient GetClient_WMDB()
        {
            ///SqlSugarClient 线程唯一
            SqlSugarClient db = CallContext<SqlSugarClient>.GetData("SugarClientWMDB");
            if (db == null)
            {
                //设置缓存
                db = new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = ConnectString_MasterDB,
                    DbType = DbType.MySql,
                    IsAutoCloseConnection = true,
                    InitKeyType = InitKeyType.SystemTable
                });


                #region AOP 操作
                //AOP 框架
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    //sql执行前
                    //if (db.TempItems == null) db.TempItems = new Dictionary<string, object>();
                    //db.TempItems.Add("logTime", DateTime.Now);
                };
                db.Aop.OnLogExecuted = (sql, pars) =>
                {
                    //sql执行后
                    //var startingTime = db.TempItems["logTime"];
                    //db.TempItems.Remove("time");
                db.Aop.OnError = (exp) =>
                {
                    //执行SQL 错误事件
                };
                //db.Aop.OnExecutingChangeSql = (sql, pars) =>
                //{
                //    //SQL执行前 可以修改SQL
                //    return new KeyValuePair<string, SugarParameter[]>(sql, pars);
                //};
                #endregion

                CallContext<SqlSugarClient>.SetData("SugarClientWMDB", db);
                    //var completedTime = DateTime.Now;
                };
            }
            return db;
        }
    }
}
