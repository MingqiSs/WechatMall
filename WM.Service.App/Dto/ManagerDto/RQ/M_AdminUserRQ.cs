using System;
using System.Collections.Generic;
using System.Text;

namespace WM.Service.App.Dto.ManagerDto.RQ
{
    public class M_AdminUserRQ
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
