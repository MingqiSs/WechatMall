using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.Infrastructure.DEncrypt;
using WM.Infrastructure.Models;
using WM.Infrastructure.Utilities;
using WM.Service.App.Dto.WebDto.RP;
using WM.Service.App.Dto.WebDto.RQ;
using WM.Service.App.Interface;
using WM.Service.Domain.Entities;
using WM.Service.Domain.Interface;
using X.Models.WMDB;

namespace WM.Service.App
{
    public class UserService : BaseSerivce , IUserService
    {
        private readonly X.IRespository.DBSession.IWMDBSession _ibll;
        private readonly IUserDomainService _userDomainService;
        public UserService(X.IRespository.DBSession.IWMDBSession ibll, IUserDomainService userDomainService)
        {
            _ibll = ibll;
            _userDomainService = userDomainService;
        }
        public ResultDto<UserInfoRP> GetUserInfo(string uid)
        {
            var user = _userDomainService.GetUserByUID(uid);
            if (user == null) return Result<UserInfoRP>(ResponseCode.sys_token_invalid, "获取用户信息错误");
             
            return Result(new UserInfoRP { 
            uid=user.UID,
            Mobile=user.Mobile,
            BirthDate=user.BirthDate,
            Email=user.Email,
            HeadImg=user.HeadImg,
            });
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        public ResultDto<UserLoginRP> Login(UserLoginRQ rq)
        {
            var result = new ResultDto<UserLoginRP>(ResponseCode.sys_fail, "登录失败(-1)");
            var pwd = AESEncrypt.Encrypt(rq.Password, AESEncrypt.pwdKey);
            var r = _ibll.wm_user.Where(q => q.Mobile == rq.Name && q.Pwd == pwd)
                .Select(q => new UserLoginRP
                {
                    uid = q.UID,
                    Email = q.Email,
                    Mobile = q.Mobile,
                    Name = q.Name,
                    NickName=q.Nickname
                }).First();
            if (r == null)
            {
                result.Msg = "账户或密码错误!";
                return result;
            }

            return Result(r);
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        public ResultDto<bool> Register(RegisterRQ rq)
        {
            var result = new ResultDto<bool>(ResponseCode.sys_fail, "注册失败");
            rq.Name = rq.Name.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(rq.Name))
            {
                result.Ec = ResponseCode.sys_verify_failed;
                result.Msg = "手机号码不能为空";
                return result;
            }

            if (!RegexHelper.Check(rq.Name, EnumPattern.Mobile))
            {
                result.Ec = ResponseCode.sys_verify_failed;
                result.Msg = "手机号码格式不正确";
                return result;
            }

            if (string.IsNullOrWhiteSpace(rq.Pwd))
            {
                result.Ec = ResponseCode.sys_verify_failed;
                result.Msg = "登录密码不能为空";
                return result;
            }

            if (!RegexHelper.Check(rq.Pwd, EnumPattern.Password))
            {
                result.Ec = ResponseCode.sys_verify_failed;
                result.Msg = "密码必须由6-12位数字和字母组成";
                return result;
            }

            if (rq.Pwd != rq.PwdConfirm)
            {
                result.Ec = ResponseCode.sys_verify_failed;
                result.Msg = "前后密码不一致";
                return result;
            }

            if (string.IsNullOrWhiteSpace(rq.Code))
            {
                result.Ec = ResponseCode.sys_verify_failed;
                result.Msg = "请输入接收到的验证码";
                return result;
            }
            //if (!CheckValidationCode(rq.Name, rq.Code, head.IP))
            //{
            //    result.Ec = ResponseCode.sys_verify_failed;
            //    result.Msg = "请输入正确的验证码";
            //    return result;
            //}
            if (_ibll.wm_user.Where(q => q.Mobile == rq.Name).Any())
            {
                result.Msg = "该手机号码已注册!";
                return result;
            }

            var user = new wm_user()
            {
                UID = Guid.NewGuid().ToString(),
                Mobile = rq.Name,
                Pwd = AESEncrypt.Encrypt(rq.Pwd, AESEncrypt.pwdKey),
                HeadImg = "",
                Name = "",
                DataStatus = (byte)DataStatus.Enable,
                CreateTime = DateTime.Now,
                Email = "",
                WeChatAppID = "",
            };
            var isSave = _ibll.wm_user.Add(user);
            return Result(isSave);
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rq"></param>
        /// <returns></returns>
        public ResultDto<bool> ModifyUserInfo(string uid,ModifyUserInfoRQ rq)
        {
           var user= _userDomainService.GetUserByUID(uid);
           
            if (user == null) return Result<bool>(ResponseCode.sys_token_invalid, "获取用户信息错误");
            if (!string.IsNullOrWhiteSpace(rq.Email))
            {
                if(!RegexHelper.Check(rq.Email, EnumPattern.Email))
                    return Result<bool>(ResponseCode.sys_token_invalid, "邮箱格式错误");
                user.Email = rq.Email;
            } 
            if(!string.IsNullOrWhiteSpace(rq.HeadImg)) user.HeadImg = rq.HeadImg;
            if (!string.IsNullOrWhiteSpace(rq.UserName)) user.Name = rq.UserName;
            if (!string.IsNullOrWhiteSpace(rq.NickName)) user.Name = rq.NickName;
            if (rq.BirthDate.HasValue) user.BirthDate = rq.BirthDate;

           var isSave=  _ibll.wm_user.Update(user);
            return Result(isSave);
        }
        /// <summary>
        /// 获取收货地址
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public ResultDto<List<UserShoppingAddressRP>> GetUserShoppingAddress(string uid)
        {
            var user = _userDomainService.GetUserByUID(uid);
            if (user == null) return Result<List<UserShoppingAddressRP>>(ResponseCode.sys_token_invalid, "获取用户信息错误");

          

            var list= _ibll.wm_user_shopping_address.Where(q => q.UID ==uid && q.DataStatus == (byte)DataStatus.Enable)
                                           .Select(q=>new UserShoppingAddressRP { 
                                           AddressID=q.ID,
                                           isDef=q.Isdef,
                                           Receiver_Name=q.Receiver_Name,
                                           Receiver_Phone=q.Receiver_Phone,
                                           Receiver_Address=q.Receiver_Address,
                                           CityID=q.CityID,
                                           DistrictID=q.DistrictID,
                                           ProvinceID=q.ProvinceID,
                                           }).OrderBy(q=>q.AddressID,SqlSugar.OrderByType.Desc).ToList();

            var citys = _ibll.cm_city.ToList();

            var provinces = _ibll.cm_province.ToList();

            list.ForEach(q => q.Receiver_Address =$"{provinces.Where(j=>j.ID==q.ProvinceID).Select(q=>q.Name).FirstOrDefault()}{citys.Where(j => j.ID == q.CityID).Select(q => q.Name).FirstOrDefault()}{q.Receiver_Address}");//拼接省市区
            return Result(list);
        }
        /// <summary>
        /// 添加收货地址
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rq"></param>
        /// <returns></returns>
        public ResultDto<bool> AddOrUpdateUserShoppingAddress(string uid, UserShoppingAddressRQ rq)
        {
            var user = _userDomainService.GetUserByUID(uid);
            if (user == null) return Result<bool>(ResponseCode.sys_token_invalid, "获取用户信息错误");
            if(rq.Receiver_Name.IsNullOrWhiteSpace())
                return Result<bool>(ResponseCode.sys_param_format_error, "联系人不能为空");
            if (rq.Receiver_Phone.IsNullOrWhiteSpace())
                return Result<bool>(ResponseCode.sys_param_format_error, "联系电话不能为空");
            if (rq.Receiver_Address.IsNullOrWhiteSpace())
                return Result<bool>(ResponseCode.sys_param_format_error, "联系地址不能为空");
            if (rq.ProvinceID<=0)
                return Result<bool>(ResponseCode.sys_param_format_error, "省份不能为空");
            if (rq.CityID <= 0)
                return Result<bool>(ResponseCode.sys_param_format_error, "城市不能为空");
            var isSave = false;
            if (rq.isDef)
            {
                var tranSql = $"update {nameof(wm_user_shopping_address)} set Isdef=0 where UID='{uid}'";
                 _ibll.Sql_ExecuteCommand(tranSql);
            }
            if (rq.AddressID == 0)
            {
                var model = new wm_user_shopping_address
                {
                    UID = uid,
                    CityID = rq.CityID,
                    ProvinceID = rq.ProvinceID,
                    DistrictID = rq.DistrictID,
                    DataStatus = (byte)DataStatus.Enable,
                    Receiver_Address = rq.Receiver_Address,
                    Receiver_Name = rq.Receiver_Name,
                    Receiver_Phone = rq.Receiver_Phone,
                    Isdef = rq.isDef,
                    CreateTime = DateTime.Now,
                };
                //新增
                isSave = _ibll.wm_user_shopping_address.Add(model);

            }
            else {
                var addressModel = _ibll.wm_user_shopping_address.Where(q => q.UID == uid && q.ID == rq.AddressID).First();
                if(addressModel==null) return Result<bool>(ResponseCode.sys_param_format_error, "未找到修改的地址信息");
                addressModel.Receiver_Address = rq.Receiver_Address;
                addressModel.Receiver_Name = rq.Receiver_Name;
                addressModel.Receiver_Phone = rq.Receiver_Phone;
                addressModel.Isdef = rq.isDef;
                addressModel.CityID = rq.CityID;
                addressModel.ProvinceID = rq.ProvinceID;
                addressModel.DistrictID = rq.DistrictID;
                addressModel.ModifyTime = DateTime.Now;
                //新增
                isSave = _ibll.wm_user_shopping_address.Update(addressModel);
            }


            
            return Result(isSave);
        }
    }
}
