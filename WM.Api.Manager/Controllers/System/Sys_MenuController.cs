using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WM.Infrastructure.Controllers.Basic;
using WM.Infrastructure.Enums;
using WM.Infrastructure.Filters;
using WM.Service.App.Dto.ManagerDto.RP;
using WM.Service.App.Interface.System;
using X.Models.WMDB;

namespace WM.Api.Manager.Controllers.System
{
    /// <summary>
    /// 系统菜单
    /// </summary>
    [Route("api/menu")]
    [ApiController]
    public class Sys_MenuController : ApiBaseController<ISys_MenuService>
    {
        /// <summary>
        /// 系统菜单
        /// </summary>
        /// <param name="service"></param>
        public Sys_MenuController(ISys_MenuService service)
               : base(service)
        {

        }
        /// <summary>
        /// 获取当前用户菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet, HttpPost, Route("getTreeMenu")]
        [ProducesResponseType(typeof(List<M_AdminRoleMenuRP>), 200)]
        public async Task<IActionResult> GetTreeMenu()
        {
            return Ok(await Service.GetCurrentMenuList());
        }
        /// <summary>
        /// 获取当前用户菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet, HttpPost, Route("getMenu")]
        public async Task<IActionResult> GetMenu()
        {
            return Ok(await Service.GetMenuList());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        [HttpPost, Route("getTreeItem")]
        [ApiActionPermission("Sys_Menu", "1", ActionPermissionOptions.Search)]
        public async Task<IActionResult> GetTreeItem(int menuId)
        {
            return Ok(await Service.GetTreeInfo(menuId));
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="menu"></param>
       /// <returns></returns>
        [HttpPost, Route("save"), ApiActionPermission(1)]
        [ApiActionPermission("Sys_Menu", "1", ActionPermissionOptions.Search)]
        public async Task<IActionResult> Save(Sys_Menu menu)
        {
            return Ok(await Service.Save(menu));
        }
    }
}