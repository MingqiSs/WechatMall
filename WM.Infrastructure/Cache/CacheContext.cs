using System;
using System.Collections.Generic;
using System.Text;
using WM.Infrastructure.Config;

namespace WM.Infrastructure.Cache
{
    public class CacheContext
    {
        private static object MemorLocker = new object();
        private static object RedisLocker = new object();
        static string RedisConnectString = AppSetting.GetConfig("Cache:RedisConnectionString");
        static string RedisConnectPwd = AppSetting.GetConfig("Cache:ConnectionPwd");
        static int RedisDB = Convert.ToInt32(AppSetting.GetConfig("Cache:Defaultdb"));
        static NewLife.Caching.ICache _MemoryCache;
        static NewLife.Caching.ICache _RedisCache;
        /// <summary>
        /// 进程内缓存
        /// </summary>
        public static NewLife.Caching.ICache MemoryCache
        {
            get
            {
                if (null != _MemoryCache) return _MemoryCache;
                lock (MemorLocker)
                {
                    if (null == _MemoryCache)
                    {
                        _MemoryCache = NewLife.Caching.MemoryCache.Default;
                    }
                }
                return _MemoryCache;
            }
        }
        /// <summary>
        /// Redis 缓存
        /// </summary>
        public static NewLife.Caching.ICache RedisCache
        {
            get
            {
                if (null != _RedisCache) return _RedisCache;
                lock (RedisLocker)
                {
                    if (null == _RedisCache)
                    {
                        NewLife.Caching.FullRedis.Register();
                        
                        _RedisCache =new  NewLife.Caching.Redis(RedisConnectString, RedisConnectPwd??"", RedisDB);

                    }
                }
                return _RedisCache;
            }
        }
        /// <summary>
        /// MemoryCache 获取或创建
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="key"></param>
        /// <param name="action"></param>
        /// <param name="expire">缓存时间秒，小于0时使用默认缓存时间</param>
        /// <returns></returns>
        public static TResult GetCache<TResult>(string key, Func<TResult> action, int expire = -1)
        {
            TResult result = default(TResult);
            //1.0 从缓存中取KEY对应的数据，如果不存在则使用action回调获取，并缓存起来
            if (MemoryCache.ContainsKey(key))
            {
                return MemoryCache.Get<TResult>(key);
            }
            else
            {
                result = action();
                MemoryCache.Set<TResult>(key, result, expire);
                return result;
            }
        }
        /// <summary>
        /// RedisCache 获取或创建
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="key"></param>
        /// <param name="action"></param>
        /// <param name="expire">缓存时间秒，小于0时使用默认缓存时间</param>
        /// <returns></returns>
        public static TResult GetRedisCache<TResult>(string key, Func<TResult> action, int expire = -1)
        {
            TResult result = default(TResult);
            //1.0 从缓存中取KEY对应的数据，如果不存在则使用action回调获取，并缓存起来
            if (RedisCache.ContainsKey(key))
            {
                return RedisCache.Get<TResult>(key);
            }
            else
            {
                result = action();
                RedisCache.Set<TResult>(key, result, expire);
                return result;
            }
        }
        public static object RandomLock = new object();
        /// <summary>
        /// 获取随机的缓存时间
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int GetRandom(int min, int max)
        {
            int result = min;
            lock (RandomLock)
            {
                System.Threading.Thread.Sleep(6);
                Random r = new Random(unchecked((int)DateTime.Now.Ticks));
                int n = r.Next(0, max - min);
                result += n;
            }
            return result;
        }
    }
}
