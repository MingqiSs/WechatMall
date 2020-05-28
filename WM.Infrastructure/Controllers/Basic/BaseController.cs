using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WM.Infrastructure.Models;

namespace WM.Infrastructure.Controllers.Basic
{
    public class BaseController<IServiceBase> : ControllerBase
    {
        protected IServiceBase Service;
        //private ResultDto Result { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public BaseController()
        {

        }
        public BaseController(IServiceBase service)
        {
            Service = service;
        }
        public BaseController(string projectName, string folder, string tablename, IServiceBase service)
        {
            Service = service;
        }
        [ApiExplorerSettings(IgnoreApi = true)]//todo: 打上标记 swagger不会报错
        public virtual async Task<ActionResult> Del(KeyOptions keys)
        {
            var r = await Task.FromResult(InvokeService("Del", new object[] { keys }));
          
            return Ok(r);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public virtual async Task<ActionResult> GetPageData(PageDataOptions loadData)
        {
            var r = await Task.FromResult(InvokeService("GetPageData", new object[] { loadData }));
            return Ok(r);
        }
        /// <summary>
        /// 反射调用service方法
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private object InvokeService(string methodName, object[] parameters)
        {
            return Service.GetType().GetMethod(methodName).Invoke(Service, parameters);
        }
        /// <summary>
        /// 反射调用service方法
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="types">为要调用重载的方法参数类型：new Type[] { typeof(SaveDataModel)</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private object InvokeService(string methodName, Type[] types, object[] parameters)
        {
            return Service.GetType().GetMethod(methodName, types).Invoke(Service, parameters);
        }
    }
}
