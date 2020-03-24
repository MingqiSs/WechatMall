using System;
using System.Collections.Generic;
using System.Text;
using WM.Infrastructure.Models;

namespace WM.Service.App
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseSerivce
    {
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


      
       
       
    }
}
