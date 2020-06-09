using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using WM.Infrastructure.Config;
using WM.Infrastructure.Dapper;

namespace WM.Infrastructure.DBManager
{
    public class DBServerProvider : DapperBaseMysql
    {
        public static string ConnectionString = AppSetting.GetConfig("ConnectionStrings:WMDB");
        public override string NewConnect => ConnectionString;

      

    }
}
