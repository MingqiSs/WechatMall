using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace WM.Infrastructure.Dapper
{
    public abstract class DapperBaseMysql : IDisposable
    {
        /// <summary>
        /// 初始化数据库连接
        /// </summary>
        public abstract string NewConnect
        {
            get;
        }
        protected IDbConnection _connection;
        protected IDbConnection connection => _connection ?? (_connection = GetOpenConnection());

        public IDbConnection GetOpenConnection(bool mars = false)
        {
            var cs = NewConnect;
            if (mars)
            {
                var scsb = new SqlConnectionStringBuilder(cs)
                {

                    MultipleActiveResultSets = true
                };
                cs = scsb.ConnectionString;
            }
            var connection = new MySqlConnection(cs);
            connection.Open();
            return connection;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        public MySqlConnection GetClosedConnection()
        {
            var conn = new MySqlConnection(NewConnect);
            if (conn.State != ConnectionState.Closed) throw new InvalidOperationException("should be closed!");
            return conn;
        }
        /// <summary>
        /// 简单事务操作
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public bool TransService(params string[] sqls)
        {
            using (var conn = GetOpenConnection())
            {
                IDbTransaction transaction = conn.BeginTransaction();
                try
                {
                    foreach (var sql in sqls)
                    {
                        int n = conn.Execute(sql, null, transaction);
                    }
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

    }
}
