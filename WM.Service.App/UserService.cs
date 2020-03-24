using System;
using System.Collections.Generic;
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
                WeChatID = "",
            };
            var isSave = _ibll.wm_user.Add(user);
            return Result(isSave);
        }
        /// 修改用户信息
        /// </summary>
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
            if (!string.IsNullOrWhiteSpace(rq.UserName)) user.HeadImg = rq.UserName;
            if (rq.BirthDate.HasValue) user.BirthDate = rq.BirthDate;

           var isSave=  _ibll.wm_user.Update(user);
            return Result(isSave);
        }
    }
}
