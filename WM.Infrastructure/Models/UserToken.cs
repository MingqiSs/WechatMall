using System;

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


    public class AdminToken {
        /// <summary>
        /// 管理员Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public int Role { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string RoleCode { get; set; }
        /// <summary>
        /// 管理员名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ip
        /// </summary>
        public string IP { get; set; }
    }

}
