using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WM.Api.Manager.Filter
{
    /// <summary>
    /// 控制权限
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CheckRoleAuthorizeFilter : Attribute, IAsyncAuthorizationFilter
    {   /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            //  var _managerService = context.HttpContext.RequestServices.GetRequiredService<IManagerService>();
            var isDefined = false;
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                isDefined = controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true)
                   .Any(a => a.GetType().Equals(typeof(AllowAnonymousAttribute)));
            }
            if (isDefined) return;

            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = new ChallengeResult();
                return;
            }
            //var roleCode = context.HttpContext.User.GetAdminToken().RoleCode.ToLower();
            //var manageRole = _managerService.GetManageRoleAsync(roleCode).Result.Dt;

            //if (manageRole.Mode == 0)
            //{
            //    return;
            //}
            //else if (manageRole.Mode == 1)
            //{
            //    var controller = context.RouteData.Values["controller"].ToString().ToLower();
            //    if (manageRole.AllowRouteData.Contains(controller))
            //    {
            //        return;
            //    }
            //}
            //else if (manageRole.Mode == 2)
            //{
            //    var path = context.HttpContext.Request.Path.Value.ToLower();
            //    if (manageRole.AllowRouteData.ToLower().Contains(path))
            //    {
            //        return;
            //    }
            //}
            HandleException(context, "无此操作权限");
            return;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="returnCode"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private static void HandleException(AuthorizationFilterContext context, string msg)
        {
            var path = context.HttpContext.Request.Path.Value.ToLower();

            var data = new { res = 0, ec = 1, msg, dt = false };

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            context.HttpContext.Response.ContentType = "application/json;charset=utf-8";
            context.Result = new JsonResult(data);
        }
    }
}
