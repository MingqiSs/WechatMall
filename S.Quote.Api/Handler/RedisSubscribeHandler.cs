using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S.Quote.Api.Handler
{
    public class RedisSubscribeHandler
    {
        private static IConnectionMultiplexer _connection;
        public RedisSubscribeHandler()
        {
            _connection = ConnectionMultiplexer.Connect("192.168.70.236:6379");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subChannael"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public async Task SubscribeAsync(string subChannael, Action<RedisChannel, RedisValue> callback)
        {
            var sub = _connection.GetSubscriber();
            await sub.SubscribeAsync(subChannael, (channel, message) =>
            {
                callback(channel, message);
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        public async Task Unsubscribe(string channel)
        {
            ISubscriber sub = _connection.GetSubscriber();
            await sub.UnsubscribeAsync(channel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task UnsubscribeAll()
        {
            ISubscriber sub = _connection.GetSubscriber();
            await sub.UnsubscribeAllAsync();
        }
    }
}
