using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.Common;

namespace X.TConsole
{
    public static class ProjectInit
    {
        public const string DBMaster = "WMDB";

        /// <summary>
        /// 从数据库生成对应的表和视图的实体对象模型
        /// 默认生成目录：/X.Models/TableEntities/
        /// </summary>
        public static void CreateDBClassFile()
        {
            Console.WriteLine("初始化实体模型...");
            //生成库对应实体
            var db2 = X.Respository.DBOperation.GetClient_WMDB();

            var dics = new Dictionary<DbTableInfo, List<DbColumnInfo>> { };

            var tables = db2.DbMaintenance.GetTableInfoList();
            for (int i = 0; i < tables.Count; i++)
            {
                dics.Add(tables[i], db2.DbMaintenance.GetColumnInfosByTableName(tables[i].Name));
            };

            var views = db2.DbMaintenance.GetViewInfoList();
            for (int i = 0; i < views.Count; i++)
            {
                dics.Add(views[i], db2.DbMaintenance.GetColumnInfosByTableName(views[i].Name));
            };

            ClassOperation.InitModel(dics, "../X.Models/WMDB/", "X.Models.WMDB");
            Console.WriteLine("WMDB...Finish");
        }

        /// <summary>
        /// 2.1 初始化接口
        /// </summary>
        public static void InitIRespository()
        {
            #region 1.0 生成实体接口
            //生成库对应接口
            var db2 = X.Respository.DBOperation.GetClient_WMDB();
            var TableList2 = db2.DbMaintenance.GetTableInfoList();
            var ViewList2 = db2.DbMaintenance.GetViewInfoList();
            var tlist2 = new List<SqlSugar.DbTableInfo>();
            tlist2.AddRange(TableList2);
            tlist2.AddRange(ViewList2);
            var tablenames2 = tlist2.Select(s => s.Name).ToList();
            ClassOperation.InitIRespository(tablenames2, "../X.IRespository/Sons/", "X.IRespository.Sons", ProjectInit.DBMaster);
            #endregion
        }
        /// <summary>
        /// 2.2 初始化仓储数据访问层接口
        /// </summary>
        public static void InitIRespositorySession()
        {
            var db2 = X.Respository.DBOperation.GetClient_WMDB();
            var TableList2 = db2.DbMaintenance.GetTableInfoList();
            var ViewList2 = db2.DbMaintenance.GetViewInfoList();
            var tlist2 = new List<SqlSugar.DbTableInfo>();
            tlist2.AddRange(TableList2);
            tlist2.AddRange(ViewList2);
            var tablenames2 = tlist2.Select(s => s.Name).ToList();
            ClassOperation.InitIRespositorySession(tablenames2, "../X.IRespository/DBSession/", "X.IRespository.DBSession", ProjectInit.DBMaster);
        }
        /// <summary>
        /// 3.1 初始化仓储数据访问层
        /// </summary>
        public static void InitRespository()
        {
            var db2 = X.Respository.DBOperation.GetClient_WMDB();
            var TableList2 = db2.DbMaintenance.GetTableInfoList();
            var ViewList2 = db2.DbMaintenance.GetViewInfoList();
            var tlist2 = new List<SqlSugar.DbTableInfo>();
            tlist2.AddRange(TableList2);
            tlist2.AddRange(ViewList2);
            var tablenames2 = tlist2.Select(s => s.Name).ToList();
            ClassOperation.InitRespository(tablenames2, "../X.Respository/Sons/", "X.Respository.Sons", ProjectInit.DBMaster);
        }
        /// <summary>
        /// 3.2初始化仓储数据模型
        /// </summary>
        public static void InitRespositorySession()
        {


            var db2 = X.Respository.DBOperation.GetClient_WMDB();
            var TableList2 = db2.DbMaintenance.GetTableInfoList();
            var ViewList2 = db2.DbMaintenance.GetViewInfoList();
            var tlist2 = new List<SqlSugar.DbTableInfo>();
            tlist2.AddRange(TableList2);
            tlist2.AddRange(ViewList2);
            var tablenames2 = tlist2.Select(s => s.Name).ToList();
            ClassOperation.InitRespositorySession(tablenames2, "../X.Respository/DBRespository/", "X.Respository.DBRespository", ProjectInit.DBMaster);
        }
        /// <summary>
        /// 4.1 单独初始化表接口
        /// </summary>
        /// <param name="tblist"></param>
        /// <param name="dbname"></param>
        public static void InitTableIRespository(List<string> tblist, string dbname)
        {
            List<string> tablenames = tblist;
            ClassOperation.InitIRespository(tablenames, "../X.IRespository/Sons/", "X.IRespository.Sons", dbname);
            ClassOperation.InitIRespositorySession(tablenames, "../X.IRespository/DBSession/", "X.IRespository.DBSession", dbname);
        }
        /// <summary>
        /// 4.2 初始化表仓储
        /// </summary>
        /// <param name="tblist"></param>
        /// <param name="dbname"></param>
        public static void InitTableRespository(List<string> tblist, string dbname)
        {
            List<string> tablenames = tblist;
            ClassOperation.InitRespository(tablenames, "../X.Respository/Sons/", "X.Respository.Sons", dbname);
            ClassOperation.InitRespositorySession(tablenames, "../X.Respository/DBRespository/", "X.Respository.DBRespository", dbname);
        }
    }
}
