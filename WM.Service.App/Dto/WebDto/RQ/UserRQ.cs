using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WM.Service.App.Dto.WebDto.RQ
{
  public  class UserLoginRQ
    {
        /// <summary>
        /// 账号(手机号)
        /// </summary>
        [Required]
        [JsonProperty("Name")]
        public string Name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }
    }
    public class RegisterRQ {
        /// <summary>
        /// 账号(手机号)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        /// 确认密码
        /// </summary>
        public string PwdConfirm { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 推荐码 没有则不填
        /// </summary>
      //  public string ReferInvitationCode { get; set; }

    }
    /// <summary>
    /// 
    /// </summary>
    public class ModifyUserInfoRQ
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? BirthDate { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImg { get; set; }
    }
}
