using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WM.Infrastructure.Models;
using WM.Service.App.Services.System;

namespace WM.Service.App.Interface.System
{
    /// <summary>
    /// 角色
    /// </summary>
   public interface ISys_RoleService
    {
        /// <summary>
        ///获取当前用户下的所有角色与当前用户的菜单权限
        /// </summary>
        /// <returns></returns>
        Task<WebResponseContent> GetCurrentTreePermission();
        /// <summary>
        /// 获取角色当前权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<WebResponseContent> GetUserTreePermission(int roleId);
        /// <summary>
        /// 保存用戶权限
        /// </summary>
        /// <param name="userPermissions"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<WebResponseContent> SavePermission(List<UserPermissions> userPermissions, int roleId);
    }
}
