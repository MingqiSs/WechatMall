using MC.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using WM.Infrastructure.Config;
using WM.Infrastructure.Enums;
using WM.Infrastructure.Extensions;
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

            return responseContent.OK("登录成功", new M_AdminUserRP
            {
                id = user.UID,
                UserName = user.UserName,
                RoleId = user.Role_Id,
                HeadImageUrl = user.HeadImageUrl,
                Moblie = user.Mobile,
                Email = user.Email,
                Token = token,
            }); 
        }
        /// <summary>
        /// 修改系统用户资料
        /// </summary>
        /// <param name="saveModel"></param>
        /// <returns></returns>
        //public override WebResponseContent Update(SaveModel saveModel) {
          
        //    if (saveModel == null)
        //        return Response.Error(ResponseType.ParametersLack);
        //    if (saveModel.MainData.Count <= 1) return Response.Error("系统没有配置好编辑的数据，请检查model!");
            
        //    //设置修改时间,修改人的默认值
        //    var userInfo = UserContext.Current.UserInfo;
        //    // 设置默认字段的值"CreateID", "Creator", "CreateDate"，"ModifyID", "Modifier", "ModifyDate"
        //    saveModel.MainData.Add("Modifier", userInfo.UserName);
        //    saveModel.MainData.Add("ModifyDate", DateTime.Now);
        //    Sys_User user = saveModel.MainData.DicToEntity<Sys_User>();

        //    Response.Status = repository.Sys_User.Update(q => new Sys_User {
        //    UserTrueName=user.UserTrueName,
        //    }, q => q.UID == user.UID);
        //    if(Response.Status) return Response.OK(ResponseType.EidtSuccess);
           
        //    return Response;
        //}
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="saveModel"></param>
        /// <returns></returns>
        //public override WebResponseContent Add(SaveModel saveModel)
        //{
        //    if (saveModel == null)
        //        return Response.Error(ResponseType.ParametersLack);
        //    if (saveModel.MainData.Count <= 1) return Response.Error("系统没有配置好编辑的数据，请检查model!");

        //    //设置修改时间,修改人的默认值
        //    var userInfo = UserContext.Current.UserInfo;
        //    // 设置默认字段的值"CreateID", "Creator", "CreateDate"，"ModifyID", "Modifier", "ModifyDate"
        //    //  saveModel.MainData.Add("Creator", userInfo.UserName);
        //    // saveModel.MainData.Add("CreateDate", DateTime.Now);
        //    Sys_User user = saveModel.MainData.DicToEntity<Sys_User>();
        //    user.UID = Guid.NewGuid().ToString();
        //    user.Creator = userInfo.UserName;
        //    user.CreateDate = DateTime.Now;
        //    Response.Status = repository.Sys_User.Add(user);
        //    if (Response.Status) return Response.OK(ResponseType.SaveSuccess);

        //    return Response;
        //}
    }
}
