using System;
using System.Collections.Generic;
using System.Text;
using WM.Infrastructure.Models;
using WM.Service.App.Dto.ManagerDto.RP;

namespace WM.Service.App.Interface
{
   public interface Sys_UserService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        ResultDto<M_AdminUserRP> AdminLogin(string userName, string password, string ip);
    }
}
