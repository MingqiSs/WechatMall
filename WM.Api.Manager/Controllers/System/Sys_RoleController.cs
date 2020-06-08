﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WM.Infrastructure.Controllers.Basic;
using WM.Infrastructure.Enums;
using WM.Infrastructure.Filters;
using WM.Service.App.Interface.System;
using WM.Service.App.Services.System;

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
        /// 获取用户相关权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPost, Route("getUserTreePermission")]
        [ApiActionPermission(ActionPermissionOptions.Search)]
        public async Task<IActionResult> GetUserTreePermission(int roleId)
        {
            return Ok(await Service.GetUserTreePermission(roleId));
        }
        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="userPermissions"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPost, Route("SavePermission")]
        [ApiActionPermission(ActionPermissionOptions.Update)]
        public async Task<IActionResult> SavePermission([FromBody] List<UserPermissions> userPermissions, int roleId)
        {
            return Ok(await Service.SavePermission(userPermissions, roleId));
        }
    }

}