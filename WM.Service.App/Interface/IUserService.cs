using System;
using System.Collections.Generic;
using System.Text;
using WM.Infrastructure.Models;
using WM.Service.App.Dto.WebDto.RP;
using WM.Service.App.Dto.WebDto.RQ;
using X.Models.WMDB;

namespace WM.Service.App.Interface
{
    public interface IUserService
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        ResultDto<UserInfoRP> GetUserInfo(string uid);
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        ResultDto<UserLoginRP> Login(UserLoginRQ rq);
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        ResultDto<bool> Register(RegisterRQ rq);
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rq"></param>
        /// <returns></returns>
        ResultDto<bool> ModifyUserInfo(string uid, ModifyUserInfoRQ rq);
        /// <summary>
        /// 获取收货地址
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        ResultDto<List<UserShoppingAddressRP>> GetUserShoppingAddress(string uid);
        /// <summary>
        /// 添加或修改收货地址
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rq"></param>
        /// <returns></returns>
        ResultDto<bool> AddOrUpdateUserShoppingAddress(string uid, UserShoppingAddressRQ rq);
    }
}
