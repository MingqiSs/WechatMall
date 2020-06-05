using System;
using System.Collections.Generic;
using System.Text;
using WM.Infrastructure.Extensions.AutofacManager;
using WM.Infrastructure.Models;

namespace WM.Infrastructure.UserManager
{
    public class UserContext
    {
        /// <summary>
        /// 为了尽量减少redis或Memory读取,保证执行效率,将UserContext注入到DI，
        /// </summary>
        public static UserContext Current
        {
            get
            {
                return Context.RequestServices.GetService(typeof(UserContext)) as UserContext;
            }
        }

        private static Microsoft.AspNetCore.Http.HttpContext Context
        {
            get
            {
                return Utilities.HttpContext.Current;
            }
        }
        public AdminUser UserInfo
        {
            get
            {
                return Context.User.GetAdminToken();
            }
        }
        /// <summary>
        /// 角色ID为1的默认为超级管理员
        /// </summary>
        public bool IsSuperAdmin
        {
            get { return IsRoleIdSuperAdmin(RoleId); }
        }
        /// <summary>
        /// 角色ID为1的默认为超级管理员
        /// </summary>
        public static  bool IsRoleIdSuperAdmin(int roleId)
        {
            return roleId == 1;
        }
        public string UserName
        {
            get { return UserInfo.UserName; }
        }

        public string UserTrueName
        {
            get { return UserInfo.UserTrueName; }
        }

        public string Token
        {
            get { return UserInfo.Token; }
        }

        public int RoleId
        {
            get { return UserInfo.Role_Id; }
        }
    }
}
