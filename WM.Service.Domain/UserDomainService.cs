using System;
using System.Collections.Generic;
using System.Text;
using WM.Service.Domain.BaseProvider;
using WM.Service.Domain.Entities;
using WM.Service.Domain.Interface;
using X.Models.WMDB;

namespace WM.Service.Domain
{
    public class UserDomainService : BaseSerivceDomain<wm_user, X.IRespository.DBSession.IWMDBSession>, IUserDomainService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ibll"></param>
        public UserDomainService(X.IRespository.DBSession.IWMDBSession repository)
         : base(repository)
        {

        }
        /// <summary>
        /// 根据用户id获取用户信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public wm_user GetUserByUID(string uid)
        {
            return repository.wm_user.Where(q => q.UID == uid && q.DataStatus == (byte)DataStatus.Enable).First();
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public wm_user GetUserByMobileOrEmail(string name)
        {
            var delete = (byte)DataStatus.Delete;
            return repository.wm_user.Where(p => p.DataStatus != delete && (p.Mobile == name || p.Email == name)).First();
        }
    }
}
