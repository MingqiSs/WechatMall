using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using S.Quote.Api.Filter;
using S.Quote.Api.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S.Quote.Api.Extensions
{
    public static class WebSocketExtensions
    {
        public static IApplicationBuilder UseWebSocketManager(this IApplicationBuilder app)
        {
            return app.UseMiddleware<WebSocketManagerMiddleware>();
        }
        public static IServiceCollection AddWebSocketManager(this IServiceCollection services)
        {
            services.AddSingleton<RedisSubscribeHandler>();
            services.AddSingleton<WebSocketConnectionManager>();
            services.AddSingleton<WebSocketHandler>();
            return services;
        }
    }
}
