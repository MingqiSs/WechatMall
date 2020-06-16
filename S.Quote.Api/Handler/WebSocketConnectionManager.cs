using S.Quote.Api.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace S.Quote.Api.Handler
{
    public class WebSocketConnectionManager
    {
        private static ConcurrentDictionary<string, CustomWebSocket> _sockets
              = new ConcurrentDictionary<string, CustomWebSocket>();
        private object _lock = new object();
        public int GetCount()
        {
            return _sockets.Count();
        }
        public CustomWebSocket GetCustomWebSocketByKey(string key)
        {
            return _sockets.FirstOrDefault(p => p.Key == key).Value;
        }

        public void UpdateCustomWebSocket(CustomWebSocket customWebSocket)
        {
            lock (_lock)
            {
                _sockets.AddOrUpdate(customWebSocket.UserName, customWebSocket, (key, oldValue) => customWebSocket);
            }
        }
        public ConcurrentDictionary<string, CustomWebSocket> GetAll()
        {
            return _sockets;
        }

        public WebSocket GetWebSocket(string key)
        {
            CustomWebSocket _socket;
            _sockets.TryGetValue(key, out _socket);
            return _socket?.WebSocket;
        }

        public string GetId(WebSocket socket)
        {
            return _sockets.FirstOrDefault(p => p.Value.WebSocket == socket).Key;
        }

        public async Task RemoveSocket(string userName)
        {
            try
            {
                CustomWebSocket customWebSocket;
                _sockets.TryRemove(userName, out customWebSocket);
                await customWebSocket.WebSocket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
            }
            catch (Exception)
            {

            }
        }

        public async Task CloseSocket(WebSocket socket)
        {
            await socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
        }

        public void AddSocket(WebSocket socket, string userName)
        {
            var customWebSocket = new CustomWebSocket
            {
                UserName = userName,
                WebSocket = socket
            };
            _sockets.AddOrUpdate(userName, customWebSocket, (key, oldValue) => customWebSocket);
        }
    }
}
