using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ubiety.Dns.Core.Common;

namespace WM.Web.Api.Filter
{
    /// <summary>
    /// 统一异常处理和返回
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="loggerFactory"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context, ILoggerFactory loggerFactory)
        {
            ILogger<ErrorHandlingMiddleware> _logger = loggerFactory.CreateLogger<ErrorHandlingMiddleware>();

            var request = context.Request;
            var pars = request.QueryString.ToString();
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                //var statusCode = context.Response.StatusCode;
                //if (ex is ArgumentException)
                //{
                //    statusCode = 200;
                //}
                //HandleException(context, ResponseCode.sys_param_format_error, ex.Message);
            }
            finally
            {
                var statusCode = context.Response.StatusCode;
                //var returnCode = ResponseCode.sys_success;
                var msg = string.Empty;
                if (statusCode == 401)
                {
                    var fromData = @"Path:{0},输入参数:{1},IP:{2},Platform:{3},ToKen:{4}";

                    var platform = request.Headers["Platform"];
                   
                    var authorizationHeader = request.Headers["Authorization"];

                    fromData = string.Format(@fromData, request.Path.ToString(), pars, request.Host, platform, authorizationHeader);

                    if (!string.IsNullOrWhiteSpace(authorizationHeader))
                    {
                        var user = string.Empty;
                        try
                        {
                            var tokenStr = authorizationHeader.ToString().Replace("Bearer ", string.Empty);
                            var token = new JwtSecurityToken(tokenStr);
                             user = token.Claims.FirstOrDefault(m => m.Type == "live").Value??string.Empty;
                        }
                        catch (Exception ex)
                        {
                            user = "0";
                        }
                        msg = $"账户:{user},{fromData}";
                       
                    }
                    else
                        msg = $",{fromData}";
                }
                #region old
              
                #endregion
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    _logger.LogError($"异常编码：{statusCode},{msg}");
                    ///    HandleException(context, returnCode, statusCode, msg);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="returnCode"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private static void HandleException(HttpContext context, ResponseCode returnCode,int  statusCode, string msg)
        {
            var data = new { res = 0, ec = returnCode, msg = msg, dt = false };
            var result = JsonConvert.SerializeObject(data);

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json;charset=utf-8";
            context.Response.WriteAsync(result, Encoding.UTF8);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class ErrorHandlingExtensions
    {
        /// <summary>
        /// 注入错误处理Middleware
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }

}