using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WM.Infrastructure.Controllers.Basic;
using WM.Service.App.Dto.ManagerDto.RP;
using WM.Service.App.Interface.System;

namespace WM.Api.Manager.Controllers.System
{
    /// <summary>
    /// 系统菜单
    /// </summary>
    [Route("api/[controller]")]
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
        [ HttpPost, Route("getTreeMenu")]
        [ProducesResponseType(typeof(List<M_AdminRoleMenuRP>), 200)]
        public async Task<IActionResult> GetTreeMenu()
        {
            return Ok(await Service.GetCurrentMenuList());
        }
    }
}