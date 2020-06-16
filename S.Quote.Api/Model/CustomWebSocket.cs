using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace S.Quote.Api.Model
{
    public class CustomWebSocket
    {
        public WebSocket WebSocket { get; set; }
        public string UserName { get; set; }
        public bool IsSubscribe { get; set; }
    }
}
