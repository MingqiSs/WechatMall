﻿using MC.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using WM.Infrastructure.Config;
using WM.Infrastructure.Extensions.AutofacManager;
using WM.Infrastructure.Models;
using WM.Infrastructure.UserManager;
using WM.Infrastructure.Utilities;
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
        public WebResponseContent Login(string userName, string password)
        {
            WebResponseContent responseContent = new WebResponseContent();
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {

                return responseContent.Error("登录错误");
            }
            // var encryptPwd = AESEncrypt.Encrypt(password, AESEncrypt.pwdKey);

            var user = repository.Sys_User.Where(q => q.Enable == (byte)DataStatus.Enable)
                              .Where(q => q.UserName == userName && q.UserPwd == password).First();
            if (user == null)
            {
                return responseContent.Error("账户或密码错误");
            }
            var adminToken = new AdminUser
            {
                User_Id = user.UID,
                Email = user.Email,
                Role_Id = user.Role_Id,
                UserName = user.UserName,
            };
            //获取token配置
           var tokenManagement= AutofacContainerModule.GetService<IOptions<TokenManagement>>().Value;

            var token = TokenHelper.CreateAdminToken(tokenManagement, adminToken);
          
            //HttpContext.Current.Response.Headers.Add("Authorization", new StringValues(token));

            GetPermissions(user.Role_Id);

            return responseContent.OK("登录成功", new M_AdminUserRP
            {
                id = user.UID,
                UserName = user.UserName,
                RoleId = user.Role_Id,
                HeadImageUrl = user.HeadImageUrl,
                Moblie = user.Mobile,
                Email = user.Email,
                Token = token,
              //  Menus = new List<M_AdminRoleMenuRP> { },
            }); 
        }
        /// <summary>
        /// 获取权限
        /// </summary>
        /// <returns></returns>
        private List<Permissions> GetPermissions(int role_Id)
        {
            var where = string.Empty;
            if (!UserContext.Current.IsRoleIdSuperAdmin(role_Id))
            {
                where = $" and b.Role_Id = {role_Id}";
            }
            var list = repository.Sql_Query<Permissions>($@"SELECT a.Menu_Id,a.ParentId,a.TableName,a.Auth, b.AuthValue from Sys_Menu a
                                                            INNER JOIN Sys_RoleAuth b
                                                            on a.Menu_Id = b.Menu_Id
                                                             where a.Enable = 1
                                                            {where}");
            UserContext.Current.Permissions = list;
            return list; 
        }      
    }
}
