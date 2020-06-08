using System;
using System.Collections.Generic;

namespace WM.Infrastructure.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class UserToken
    {
        /// <summary>
        /// 用户UID
        /// </summary>
        public string UID { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 手机区号
        /// </summary>
        public string MobileArea { get; set; }
        /// <summary>
        /// 登入类型
        /// </summary>
        public EnumLoginType LoginType { get; set; }

        /// <summary>
        /// 渠道
        /// </summary>
        public string Channel { get; set; }
        /// <summary>
        /// 手机串号
        /// </summary>
        public string IMEI { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 平台类型
        /// </summary>
        public EnumPlatformType Platform { get; set; }
    }


    public class AdminUser
    {
        public string User_Id { get; set; }
        /// <summary>
        /// 多个角色ID
        /// </summary>
        public int Role_Id { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public string UserTrueName { get; set; }
        public int Enable { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
    public class Permissions
    {
        /// <summary>
        /// 菜单id
        /// </summary>
        public int Menu_Id { get; set; }
        /// <summary>
        /// 菜单id
        /// </summary>
        public string Menu_Name { get; set; }
        /// <summary>
        /// 父级id
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 用户权限
        /// </summary>
        public string UserAuth { get; set; }
        /// <summary>
        /// 菜单权限
        /// </summary>
        public string MenuAuth { get; set; }
        /// <summary>
        /// 用户菜单权限
        /// </summary>
        public string[] UserAuthArr { get; set; }
        /// <summary>
        /// 当前用哪个胡菜单权限
        /// </summary>
        public List<Sys_Actions> Actions { get; set; }
    }
}
