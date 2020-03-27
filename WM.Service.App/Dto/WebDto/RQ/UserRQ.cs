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
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class UserShoppingAddressRQ
    {
        /// <summary>
        /// 地址id 新增时:id传0
        /// </summary>
        public int AddressID { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Receiver_Name { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        public string Receiver_Phone { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Receiver_Address { get; set; }
        /// <summary>
        /// 是否默认
        /// </summary>
        public bool isDef { get; set; }
        /// <summary>
        /// 省份id
        /// </summary>
        public int ProvinceID { get; set; }
        /// <summary>
        /// 城市id
        /// </summary>
        public int CityID { get; set; }
        /// <summary>
        /// 区域ID
        /// </summary>
        public int DistrictID { get; set; }

    }
}
