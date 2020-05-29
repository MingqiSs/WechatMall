using System;
using System.Collections.Generic;
using System.Text;
using WM.Infrastructure.Models;
using WM.Service.App.Dto.ManagerDto.RP;
using WM.Service.App.Interface;
using WM.Service.Domain.Entities;
using X.Models.WMDB;

namespace WM.Service.App.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class Sys_UserService : BaseSerivceDomain<Sys_User, X.IRespository.DBSession.IWMDBSession>, ISys_UserService
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
        /// <returns></returns>
        public WebResponseContent AdminLogin(string userName, string password)
        {
            WebResponseContent responseContent = new WebResponseContent();
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {

                return responseContent.Error("登录错误");
            }
            // var encryptPwd = AESEncrypt.Encrypt(password, AESEncrypt.pwdKey);

            var admin = repository.Sys_User.Where(q => q.DataStatus == (byte)DataStatus.Enable)
                              .Where(q => q.UserName == userName && q.UserPwd == password).First();
            if (admin == null)
            {
                return responseContent.Error("账户或密码错误");
            }
            return responseContent.OK("登录成功", new M_AdminUserRP
            {
                id = admin.UID,
                Name = admin.UserName,
                RoleId = admin.Role_Id,
                Email = admin.Email,
                Menus = new List<M_AdminRoleMenuRP> { },
            }) ;
        }
    }
}
