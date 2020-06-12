using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WM.Infrastructure.Enums;
using WM.Infrastructure.Extensions.AutofacManager;
using WM.Infrastructure.Models;
using WM.Infrastructure.UserManager;
using WM.Service.App.Interface.System;
using WM.Service.App.Services.System;

namespace WM.Api.Manager.Filter
{
    /// <summary>
    /// 
    /// </summary>
    public class ActionPermissionFilter : ActionFilterAttribute
    {
        private readonly ILogger _logger;
        private UserContext _userContext { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ActionPermissionFilter(ILogger<ActionPermissionFilter> logger,UserContext userContext)
        {
            _logger = logger;
            _userContext = userContext;
        }
        /// <summary>
        /// 拦截控制器方法
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var isDefined = false;
            // 获取请求进来的控制器与方法
            var controllerActionDescriptor = filterContext.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                 isDefined = controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true)
                  .Any(a => a.GetType().Equals(typeof(AllowAnonymousAttribute)));
            }
          
            if (isDefined || UserContext.Current.IsSuperAdmin) 
                return;

            // 获取设置的操作码（如果没设置操作码，默认不验证权限）
            var actionCode = (PermissionAttribute)controllerActionDescriptor.MethodInfo
                .GetCustomAttributes(inherit: true)
                .FirstOrDefault(t => t.GetType().Equals(typeof(PermissionAttribute)));
           
            if (actionCode != null)
            {
                //tableNmae
                if(string.IsNullOrEmpty(actionCode.TableName))  actionCode.TableName= controllerActionDescriptor.ControllerName; 

                var ResponseConten = new WebResponseContent();
                // 验证是否通过
                var permissions = AutofacContainerModule.GetService<Sys_RoleService>().GetUserPermissions();
                if (permissions == null || permissions.Count() == 0)
                {
                    filterContext.Result = new OkObjectResult(ResponseConten.Error(ResponseType.NoPermissions));
                    return;
                }
                var actionAuth = permissions.Where(x => x.TableName == actionCode.TableName.ToLower()).FirstOrDefault()?.UserAuthArr;
                if (actionAuth == null
                     || actionAuth.Count() == 0
                     || !actionAuth.Contains(actionCode.Code.SafeString()))
                {
                    filterContext.Result = new OkObjectResult(ResponseConten.Error(ResponseType.NoPermissions));
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
    /// <summary>
    /// 设置操作码（用于权限验证）
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class PermissionAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public PermCode Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="perm"></param>
        public PermissionAttribute(PermCode perm)
        {
            Code = perm;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="perm"></param>
        /// <param name="tableName"></param>
        public PermissionAttribute(PermCode perm, string tableName = "")
        {
            Code = perm;
            TableName = tableName;
        }
    }
}
    /// <summary>
     /// 
     /// </summary>
    public enum PermCode : int
    {
        /// <summary>
        /// 新增
        /// </summary>
        Add = 0,
        /// <summary>
        /// 删除
        /// </summary>
        Delete = 1,
        /// <summary>
        /// 更新
        /// </summary>
        Update = 2,
        /// <summary>
        /// 查询
        /// </summary>
        Search = 3,
        /// <summary>
        /// 导出
        /// </summary>
        Export = 4,
        /// <summary>
        /// 审核
        /// </summary>
        Audit,
        /// <summary>
        /// 上传文件
        /// </summary>
        Upload,
        /// <summary>
        /// 导入表数据Excel
        /// </summary>
        Import
    }
