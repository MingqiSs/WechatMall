using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WM.Infrastructure.Filters
{
    public class ApiAuthorizeFilter : IAuthorizationFilter
    {
        public ApiAuthorizeFilter()
        {

        }
        /// <summary>
        /// 只判断token是否正确，不判断权限
        /// 如果需要判断权限的在Action上加上ApiActionPermission属性标识权限类别，ActionPermissionFilter作权限处理
        ///(string,string,string)1、请求参数,2、返回消息，3,异常消息,4状态
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // is Microsoft.AspNetCore.Authentication.AllowAnonymousAttribute
            //if (context.Filters.Any(item => item is IAllowAnonymousFilter))
            if (context.ActionDescriptor.EndpointMetadata.Any(item => item is AllowAnonymousAttribute))
            {
                //如果使用了固定Token不过期，直接对token的合法性及token是否存在进行验证
                if (context.Filters
                    .Where(item => item is IFixedTokenFilter)
                    .FirstOrDefault() is IFixedTokenFilter tokenFilter)
                {
                    tokenFilter.OnAuthorization(context);
                    return;
                }
                //匿名并传入了token，需要将用户的ID缓存起来，保证UserHelper里能正确获取到用户信息
                if (!context.HttpContext.User.Identity.IsAuthenticated
                    && !string.IsNullOrEmpty(context.HttpContext.Request.Headers["Authorization"]))
                {
                    //context.AddIdentity();
                }
                return;
            }
        }
    }
}
