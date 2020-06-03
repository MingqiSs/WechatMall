using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WM.Service.App.Dto.ManagerDto.RP;

namespace WM.Service.App.Interface.System
{
   public interface ISys_MenuService
    {
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        Task<List<M_AdminRoleMenuRP>> GetCurrentMenuList();
    }
}
