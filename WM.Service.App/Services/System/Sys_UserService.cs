using System;
using System.Collections.Generic;
using System.Text;
using WM.Infrastructure.Models;
using WM.Service.App.Dto.ManagerDto.RP;
using WM.Service.Domain.Entities;

namespace WM.Service.App.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class Sys_UserService : BaseSerivce<X.IRespository.DBSession.IWMDBSession>, Interface.Sys_UserService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public Sys_UserService(X.IRespository.DBSession.IWMDBSession repository) : base(repository)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public ResultDto<M_AdminUserRP> AdminLogin(string userName, string password, string ip)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                return Result<M_AdminUserRP>(ResponseCode.sys_param_format_error, "登录失败");
            }
            // var encryptPwd = AESEncrypt.Encrypt(password, AESEncrypt.pwdKey);

            var admin = repository.Sys_User.Where(q => q.DataStatus == (byte)DataStatus.Enable)
                              .Where(q => q.UserName == userName && q.UserPwd == password).First();
            if (admin == null)
            {
                return Result<M_AdminUserRP>(ResponseCode.sys_param_format_error, "账号或密码错误");
            }
            return Result(new M_AdminUserRP
            {
                id = admin.UID,
                Name = admin.UserName,
                RoleId = admin.Role_Id,
                Email = admin.Email,
                Menus = new List<M_AdminRoleMenuRP> { },
            });
        }
    }
}
