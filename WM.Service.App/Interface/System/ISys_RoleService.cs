using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WM.Infrastructure.Models;

namespace WM.Service.App.Interface.System
{
    /// <summary>
    /// 角色
    /// </summary>
   public interface ISys_RoleService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<WebResponseContent> GetCurrentTreePermission();
    }
}
