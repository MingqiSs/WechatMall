using System;
using System.Collections.Generic;
using System.Text;
using WM.Service.Domain.Entities;
using WM.Service.Domain.Interface;
using X.Models.WMDB;

namespace WM.Service.Domain
{
    public class UserDomainService : IUserDomainService
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly X.IRespository.DBSession.IWMDBSession _ibll;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ibll"></param>
        public UserDomainService(X.IRespository.DBSession.IWMDBSession ibll)
        {
            _ibll = ibll;
        }
        /// <summary>
        /// 根据用户id获取用户信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public wm_user GetUserByUID(string uid)
        {
            return _ibll.wm_user.Where(q => q.UID == uid && q.DataStatus == (byte)DataStatus.Enable).First();
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public wm_user GetUserByMobileOrEmail(string name)
        {
            var delete = (byte)DataStatus.Delete;
            return _ibll.wm_user.Where(p => p.DataStatus != delete && (p.Mobile == name || p.Email == name)).First();
        }
    }
}
