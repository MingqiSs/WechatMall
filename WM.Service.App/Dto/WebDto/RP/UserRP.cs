using System;
using System.Collections.Generic;
using System.Text;
using WM.Infrastructure.Models;

namespace WM.Service.App.Dto.WebDto.RP
{
  public  class UserLoginRP
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string uid { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
       // public string PassWord { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 登录类型 1:模拟账户,2:真实账号
        /// </summary>
        public EnumLoginType Type { get; set; }
    }
    public class UserInfoRP
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string uid { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? BirthDate { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImg { get; set; }
    }
}
