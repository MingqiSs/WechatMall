using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace WM.Infrastructure.Dapper
{
    public abstract class DapperBase : IDisposable
    {
        /// <summary>
        /// 初始化数据库连接
        /// </summary>
        public abstract string NewConnect
        {
            get;
        }
        protected SqlConnection _connection;
        protected SqlConnection connection => _connection ?? (_connection = GetOpenConnection());

        public SqlConnection GetOpenConnection(bool mars = false)
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
            var connection = new SqlConnection(cs);
            connection.Open();
            return connection;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        public SqlConnection GetClosedConnection()
        {
            var conn = new SqlConnection(NewConnect);
            if (conn.State != ConnectionState.Closed) throw new InvalidOperationException("should be closed!");
            return conn;
        }
    }
}
