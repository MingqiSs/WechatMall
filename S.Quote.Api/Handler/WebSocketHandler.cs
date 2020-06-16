using Newtonsoft.Json;
using S.Quote.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace S.Quote.Api.Handler
{
    public class WebSocketHandler
    {
        public WebSocketConnectionManager WebSocketConnectionManager { get; set; }
        private const int bufferSize = 1024 * 4;
        public WebSocketHandler(WebSocketConnectionManager webSocketConnectionManager)
        {
            WebSocketConnectionManager = webSocketConnectionManager;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="key"></param>
        public void OnConnected(WebSocket socket, string key)
        {
            WebSocketConnectionManager.AddSocket(socket, key);
        }

        public CustomWebSocket GetCustomWebSocket(string key)
        {
            return WebSocketConnectionManager.GetCustomWebSocketByKey(key);
        }

        public void UpdateCustomWebSocket(CustomWebSocket customWebSocket)
        {
            WebSocketConnectionManager.UpdateCustomWebSocket(customWebSocket);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket"></param>
        /// <returns></returns>
        public async Task OnDisconnected(WebSocket socket)
        {
            Console.WriteLine("Socket 断开了");
            await WebSocketConnectionManager.RemoveSocket(WebSocketConnectionManager.GetId(socket));
        }
        public string GetId(WebSocket socket)
        {
            return WebSocketConnectionManager.GetId(socket);
        }
        /// <summary>
        /// 连接数
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            return WebSocketConnectionManager.GetCount();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="webSocket"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task SendMessageAsync<TEntity>(WebSocket webSocket, TEntity entity)
        {
            if (webSocket.State != WebSocketState.Open)
                return;

            var settings = new JsonSerializerSettings();
            settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            settings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            var Json = JsonConvert.SerializeObject(entity);
            var bytes = Encoding.UTF8.GetBytes(Json);

            await webSocket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task SendMessageToAllAsync<TEntity>(TEntity entity)
        {
            foreach (var pair in WebSocketConnectionManager.GetAll())
            {
                if (pair.Value.WebSocket.State == WebSocketState.Open)
                    await SendMessageAsync(pair.Value.WebSocket, entity);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="webSocket"></param>
        /// <returns></returns>
        public async Task ReceiveEntity<TEntity>(WebSocket webSocket, Action<WebSocketReceiveResult, TEntity> handleMessage)
        {
            var buffer = new ArraySegment<byte>(new byte[bufferSize]);
            var result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
            while (!result.EndOfMessage)
            {
                result = await webSocket.ReceiveAsync(buffer, default(CancellationToken));
            }

            var json = Encoding.UTF8.GetString(buffer.Array);
            //json = json.Replace("\0", "").Trim();
            handleMessage(result, JsonConvert.DeserializeObject<TEntity>(json, new JsonSerializerSettings()
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            }));
        }
    }
}
