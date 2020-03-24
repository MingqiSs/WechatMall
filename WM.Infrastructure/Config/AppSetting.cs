using Microsoft.Extensions.Configuration;
using WM.Infrastructure.Models;
using System.IO;
namespace WM.Infrastructure.Config
{
    public class AppSetting
    {
        private static object objLock = new object();
        private static AppSetting instance = null;

        /// <summary>
        /// 
        /// </summary>
        private IConfigurationRoot Config { get; }

        /// <summary>
        /// 
        /// </summary>
        private AppSetting()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Config = builder.Build();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static AppSetting GetInstance()
        {
            if (instance == null)
            {
                lock (objLock)
                {
                    if (instance == null)
                    {
                        instance = new AppSetting();
                    }
                }
            }

            return instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetConfig(string name)
        {
            return GetInstance().Config.GetSection(name).Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int GetConfigInt32(string name)
        {
            return GetInstance().Config.GetSection(name).Value.ToInt32(0);
        }

    }
}
