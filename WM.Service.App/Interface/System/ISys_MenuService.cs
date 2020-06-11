using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WM.Infrastructure.Models;
using WM.Service.App.Dto.ManagerDto.RP;
using X.Models.WMDB;

namespace WM.Service.App.Interface.System
{
   public interface ISys_MenuService
    {
        /// <summary>
        /// 获取用户当前菜单
        /// </summary>
        /// <returns></returns>
        Task<List<M_AdminRoleMenuRP>> GetCurrentMenuList();
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        Task<object> GetMenuList();
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <returns></returns>
        Task<object> GetTreeInfo(int menuId);
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        Task<WebResponseContent> Save(Sys_Menu menu);
    }
}
