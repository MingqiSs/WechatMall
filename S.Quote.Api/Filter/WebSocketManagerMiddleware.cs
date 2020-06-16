using Microsoft.AspNetCore.Http;
using S.Quote.Api.Handler;
using S.Quote.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace S.Quote.Api.Filter
{
    public class WebSocketManagerMiddleware
    {
        private readonly RequestDelegate _next;
        private WebSocketHandler _webSocketHandler;
        private static int MaxConnection = 2000;
        private string subscribeQuote = "QuoteMessages1";
        private RedisSubscribeHandler _redisSubscribeHandler;
        private const string routePostfix = "/quote";
        public WebSocketManagerMiddleware(RequestDelegate next,
            WebSocketHandler webSocketHandler,
                RedisSubscribeHandler redisSubscribeHandler)
        {
            _next = next;
            _webSocketHandler = webSocketHandler;
            _redisSubscribeHandler = redisSubscribeHandler;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            if (!IsWebSocket(context))
            {
                await _next.Invoke(context);
                return;
            }
            var socket = await context.WebSockets.AcceptWebSocketAsync();

            await Echo(context, socket);
        }

        //private async Task Echo(HttpContext context, WebSocket webSocket)
        //{
        //    var buffer = new ArraySegment<byte>(new byte[1024 * 4]) ;
        //    //连接
        //    WebSocketReceiveResult result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);

        //    while (webSocket.State == WebSocketState.Open)
        //    {
        //        //返回的消息
        //        var resultMsg = Encoding.ASCII.GetString(buffer.Array);

        //        var abuffer = Encoding.ASCII.GetBytes(DateTime.Now.ToLongTimeString());
        //        //发给客户端
        //        await webSocket.SendAsync(new ArraySegment<byte>(abuffer, 0, abuffer.Length), result.MessageType, result.EndOfMessage, CancellationToken.None);

        //        result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer.Array), CancellationToken.None);
        //    }
        //    //关闭连接
        //    await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        //}

        private async Task Echo(HttpContext context, WebSocket socket)
        {
            string userName = context.Request.Query["u"];
           // { "Sender":"1001","Receiver":"system","SendTime":"2020-06-09T13:37:46.7423008+08:00","Message":"","Type":"Quote"}
            _webSocketHandler.OnConnected(socket, userName);
            try
            {
                while (socket.State == WebSocketState.Open)
                {
                    await _webSocketHandler.ReceiveEntity<MessageEntity>(socket, async (result, messageEntity) =>
                    {
                        if (result.MessageType == WebSocketMessageType.Close)
                        {
                            await _webSocketHandler.OnDisconnected(socket);
                            return;
                        }

                        switch (messageEntity.Type)
                        {
                            case MessageType.Quote:
                                {
                                    var customSocket = _webSocketHandler.GetCustomWebSocket(messageEntity.Sender);
                                    if (customSocket == null)
                                    {
                                        break;
                                    }
                                    if (!customSocket.IsSubscribe)
                                    {
                                        //$"{customSocket.UserName}.{subscribeQuote}"
                                        await _redisSubscribeHandler.SubscribeAsync(subscribeQuote, (channel, message) =>
                                        {
                                            var msgEntity = new MessageEntity
                                            {
                                                Message = message,
                                                Type = MessageType.Quote,
                                                Receiver = userName,
                                                Sender = "system"
                                            };
                                            _webSocketHandler.SendMessageAsync(socket, msgEntity).ConfigureAwait(false);
                                        });
                                        customSocket.IsSubscribe = true;
                                        _webSocketHandler.UpdateCustomWebSocket(customSocket);
                                    }
                                    break;
                                }
                            case MessageType.KLine:

                                break;
                        }
                    });
                }
                if (Enum.IsDefined(typeof(WebSocketCloseStatus), socket.CloseStatus))
                {
                    //userName = _webSocketHandler.GetId(socket);
                    //await _redisSubscribeHandler.Unsubscribe($"{subscribeQuote}");
                    await _webSocketHandler.OnDisconnected(socket);
                }
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Close", default(CancellationToken));
                socket.Dispose();
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex.StackTrace);
            }
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool IsWebSocket(HttpContext context)
        {
            return context.WebSockets.IsWebSocketRequest &&
                context.Request.Path == routePostfix;
        }
    }
}
