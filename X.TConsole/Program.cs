using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using X.Common;
using X.Models;
using X.Respository;

namespace X.TConsole
{
    class Program
    {
        /// <summary>
        /// 此项目用于ORM数据实体模型的初始化，请勿随意操作
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            ////控制台依赖注入
            //IServiceCollection services = new ServiceCollection();
            ////注入
            //services.AddTransient<IRespository.DBSession.IMoShiSession, Respository.DBRespository.MoShiSession>();
            ////构建容器
            //IServiceProvider serviceProvider = services.BuildServiceProvider();
            ////解析
            //var ibll = serviceProvider.GetService<IRespository.DBSession.IMoShiSession>();
            //int total = 0;
            //var d = ibll.DB.Queryable<Site_News>().Where(s => s.IsDel == false && s.IsPublish).OrderBy(s => s.PublishDate, SqlSugar.OrderByType.Desc).Select(
            //            s => new
            //            {
            //                ID = s.ID,
            //                Cate = s.Cate,
            //                Title = s.Title,
            //                Source = s.Source,
            //                AddDate = s.AddDate,
            //                ImageUrl = s.ImageUrl,
            //                PublishDate = s.PublishDate
            //            }).ToPageList(1, 10 * 10, ref total);
            //Console.WriteLine(d.Count);


            //long startT = DateTime.Now.Ticks;
            //List<PayMertName> list = DocControl.GetPayBankListCache(bllSession_Pay).Select(s => new PayMertName() { MerNo = s.BankCode, MerName = s.BankName }).ToList();
            //long endT = DateTime.Now.Ticks;
            //string result = TimeSpan.FromTicks(endT - startT).TotalMilliseconds.ToString();
            //Console.WriteLine("数据记录条数：{0}--耗时{1}", list.Count, result);
            //for (int i = 0; i < 70; i++)
            //{
            //    long t1 = DateTime.Now.Ticks;
            //    List<PayMertName> test = DocControl.GetPayBankListCache(bllSession_Pay).Select(s => new PayMertName() { MerNo = s.BankCode, MerName = s.BankName }).ToList();
            //    long t2 = DateTime.Now.Ticks;
            //    Console.WriteLine("数据记录条数：{0}--耗时{1}", test.Count, TimeSpan.FromTicks(t1 - t2).TotalMilliseconds);
            //    Thread.Sleep(1000);
            //}

            //long startT = DateTime.Now.Ticks;
            //List<PayMertName> list = DocControl.GetPayBankListORMCache(bllSession_Pay).Select(s => new PayMertName() { MerNo = s.BankCode, MerName = s.BankName }).ToList();
            //long endT = DateTime.Now.Ticks;
            //string result = TimeSpan.FromTicks(endT - startT).TotalMilliseconds.ToString();
            //Console.WriteLine("数据记录条数：{0}--耗时{1}", list.Count, result);
            //for (int i = 0; i < 70; i++)
            //{
            //    long t1 = DateTime.Now.Ticks;
            //    List<PayMertName> test = DocControl.GetPayBankListCache(bllSession_Pay).Select(s => new PayMertName() { MerNo = s.BankCode, MerName = s.BankName }).ToList();
            //    long t2 = DateTime.Now.Ticks;
            //    Console.WriteLine("数据记录条数：{0}--耗时{1}", test.Count, TimeSpan.FromTicks(t1 - t2).TotalMilliseconds);
            //    Thread.Sleep(1000);
            //}



            //处理不同环境下Directory.GetCurrentDirectory获取路径不一致问题
            string path = Directory.GetCurrentDirectory();

            if (path.Contains(@"\bin\Debug\netcoreapp3.1"))
            {
                string newpath = path.Replace(@"\bin\Debug\netcoreapp3.1", "");
                Directory.SetCurrentDirectory(newpath);
                Console.WriteLine(Directory.GetCurrentDirectory());
            }
            Init();//初始化项目主数据库，如果有多个需要创建多个

            //InitTable();//单独初始化某个库的某些表，要求表实体自己创建

            Console.WriteLine("Finish......");
            System.Console.ReadKey();
        }

        /// <summary>
        /// 初始化项目主数据库
        /// </summary>
        static void Init()
        {
            Console.WriteLine("初始化开始......");
            //1.0 初始化数据库表实体
            ProjectInit.CreateDBClassFile();
            //2.0 初始化仓储接口
            ProjectInit.InitIRespository();
            Console.WriteLine("InitIRespository...Finish");
            //2.1 初始化仓储接口
            ProjectInit.InitIRespositorySession();
            Console.WriteLine("InitIRespositorySession...Finish");
            //3.1 初始化仓储
            ProjectInit.InitRespository();
            Console.WriteLine("InitRespository...Finish");
            //3.2 初始化仓储
            ProjectInit.InitRespositorySession();
            Console.WriteLine("InitRespositorySession...Finish");
        }
        /// <summary>
        /// 单独初始化某一张表
        /// 需要手动创建实体
        /// </summary>
        static void InitTable()
        {
            //1.0 单独初始化表，要求必须已经在X.Models中创建了对应库的文件夹和对应的实体接口
            List<string> list = new List<string>() { "MS_LoginRecord" };
            ProjectInit.InitTableIRespository(list, "PayService");
            ProjectInit.InitTableRespository(list, "PayService");
        }
        /// <summary>
        /// 依赖注入
        /// </summary>
        static void InitService()
        {
            ////控制台依赖注入
            //IServiceCollection services = new ServiceCollection();
            ////注入
            //services.AddTransient<IRespository.IRespositorySession, Respository.RespositorySession>();
            ////构建容器
            //IServiceProvider serviceProvider = services.BuildServiceProvider();
            ////解析
            //var bllSession = serviceProvider.GetService<IRespository.IRespositorySession>();
            //int n=bllSession.PayMert.Count(s=>true);
            //Console.WriteLine(n);
        }
        /// <summary>
        /// 缓存测试
        /// </summary>
        static void InitCache()
        {
            ////控制台依赖注入
            //IServiceCollection services = new ServiceCollection();
            ////注入
            //services.AddTransient<IRespository.DBSession.IPayServiceSession, Respository.DBRespository.PayServiceSession>();
            ////services.AddTransient<SqlSugar.ICacheService, CoreMemoryCache>();
            //services.AddSingleton<SqlSugar.ICacheService, RedisCache>(
            //    s =>
            //    {
            //        return new RedisCache(DBOperation.RedisCacheHost, 6379, null, -1, 15);
            //    }
            //    );
            ////构建容器
            //IServiceProvider serviceProvider = services.BuildServiceProvider();
            ////解析
            //var bllSession = serviceProvider.GetService<IRespository.DBSession.IPayServiceSession>();

            //int n = bllSession.PayBankBase.Count(s => true);
            ////-----------------------------------------------------------------------------------------------------------------
            //var cacheMan = serviceProvider.GetService<SqlSugar.ICacheService>();
            //long startT = DateTime.Now.Ticks;
            //List<PayMertName> list = bllSession.PayBankBase.where(s => true, true, 60).Select(s => new PayMertName() { MerNo = s.BankCode, MerName = s.BankName }).ToList();
            //long endT = DateTime.Now.Ticks;
            //string result = TimeSpan.FromTicks(endT - startT).TotalMilliseconds.ToString();
            //Console.WriteLine("数据记录条数：{0}--耗时{1}", list.Count, result);
            //foreach (var item in cacheMan.GetAllKey<string>())
            //{
            //    Console.WriteLine("key-string:" + item);
            //}
            //cacheMan.Add("key-test1", "ssssssssssssssssssssssssss--value", 30);
            //cacheMan.Add("key-test2", "ssssssssssssssssssssssssss--value", 30);
            //foreach (var item in cacheMan.GetAllKey<string>())
            //{
            //    Console.WriteLine("key-string:" + item);
            //}
            //for (int i = 0; i < 70; i++)
            //{
            //    long t1 = DateTime.Now.Ticks;
            //    List<PayMertName> test = bllSession.PayBankBase.where(s => true, true, 60).Select(s => new PayMertName() { MerNo = s.BankCode, MerName = s.BankName }).ToList();
            //    long t2 = DateTime.Now.Ticks;
            //    Console.WriteLine("数据记录条数：{0}--耗时{1}", test.Count, TimeSpan.FromTicks(t1 - t2).TotalMilliseconds);
            //    Thread.Sleep(1000);
            //}
        }

    }
    public class PayMertName
    {
        public string MerNo { get; set; }
        public string MerName { get; set; }
    }
}
