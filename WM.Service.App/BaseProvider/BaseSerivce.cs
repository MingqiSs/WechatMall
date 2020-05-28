using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using WM.Infrastructure.Extensions.AutofacManager;
using WM.Infrastructure.Localization;
using WM.Infrastructure.Models;

namespace WM.Service.App
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseSerivce<TRepository> : IDependency
        where TRepository : X.IRespository.DBSession.IWMDBSession //orm对象
    {
        /// <summary>
        /// 
        /// </summary>
        protected X.IRespository.DBSession.IWMDBSession repository;
        public BaseSerivce(TRepository repository)
        {
            this.repository = repository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inData"></param>
        /// <returns></returns>
        public ResultDto<T> Result<T>(T inData)
        {
            return new ResultDto<T>(inData);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        /// <param name="inData"></param>
        /// <returns></returns>
        public ResultDto<T> Result<T>(T inData, string msg)
        {
            return new ResultDto<T>(inData) { Msg = msg };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inData"></param>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public ResultDto<T> Result<T>(T inData, ResponseCode code, string msg)
        {
            return new ResultDto<T>(inData) { Ec = code, Msg = msg };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public ResultDto<T> Result<T>(ResponseCode code, string errorMsg)
        {
            return new ResultDto<T>(code, errorMsg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public ResultDto<T> Result<T>(Exception ex, string errorMsg = "")
        {
            return new ResultDto<T>(ResponseCode.sys_exception, errorMsg);
        }

        /// <summary>
        /// 获取 <paramref name="name"/> 对应的本地化字符串。
        /// </summary>
        /// <param name="name">本地化资源的名称。</param>
        /// <returns>返回本地化字符串。</returns>
        public static string L(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return name;
            try
            {
                var Language = WM.Infrastructure.Utilities.HttpContext.Current.Request.Headers["Language"].ToString();
                LocalizationHelper.InitializeLanguage(Language);
                var value = LocalizationHelper.GetString(name);
                return value;
            }
            catch (Exception ex)
            {
                var logger = AutofacContainerModule.GetService<ILogger<BaseSerivce<TRepository>>>();
                logger?.LogError(ex, $"多语言名称[{name}]未找到!");
                //写日志
            }
            return name;
        }

    }
}
