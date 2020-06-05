using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WM.Infrastructure.Controllers.Basic;
using WM.Infrastructure.Enums;
using WM.Infrastructure.Filters;
using WM.Service.App.Interface.System;

namespace WM.Api.Manager.Controllers.System
{
    [Route("api/[controller]")]
    [ApiController]
    public class Sys_RoleController : ApiBaseController<ISys_RoleService>
    {
        /// <summary>
        /// 角色
        /// </summary>
        /// <param name="service"></param>
        public Sys_RoleController(ISys_RoleService service)
               : base(service)
        {

        }
        /// <summary>
        /// 获取权限
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("getCurrentTreePermission")]
        [ApiActionPermission(ActionPermissionOptions.Search)]
        public async Task<IActionResult> GetCurrentTreePermission()
        {
            return Ok(await Service.GetCurrentTreePermission());
        }

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("getUserChildRoles")]
        [ApiActionPermission(ActionPermissionOptions.Search)]
        public async Task<IActionResult> GetUserChildRoles()
        {
            return Ok(await Service.GetCurrentTreePermission());
        }

    }

}