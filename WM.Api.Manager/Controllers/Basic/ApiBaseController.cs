using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WM.Api.Manager.Filter;
using WM.Infrastructure.Filters;
using WM.Infrastructure.Models;

namespace WM.Api.Manager.Controllers.Basic
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="IServiceBase"></typeparam>
    [Authorize]
    [ApiController]
    public class ApiBaseController<IServiceBase> : BaseController<IServiceBase>
    {   /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public ApiBaseController(IServiceBase service)
      : base(service)
        {
        }
        /// <summary>
        /// 通过key删除数据
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        [Permission(PermCode.Delete)]
        [HttpPost, Route("Del")]
        public  async Task<ActionResult> Del([FromBody] object[] keys)
        {
            var r = new KeyOptions { Key = new List<string>() };
            foreach (var item in keys)
            {
                r.Key.Add(item.ToString());
            }
            return await base.Del(r);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="loadData"></param>
        /// <returns></returns>
        [Permission(PermCode.Search)]
        [HttpPost, Route("GetPageData")]
        public new async Task<ActionResult> GetPageData(PageDataOptions loadData)
        {
            return await base.GetPageData(loadData);
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="saveModel"></param>
        /// <returns></returns>
        [Permission(PermCode.Update)]
        [HttpPost, Route("Update")]
        public new async Task<ActionResult> Update(SaveModel saveModel)
        {
            return await base.Update(saveModel);
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="saveModel"></param>
        /// <returns></returns>
        [Permission(PermCode.Add)]
        [HttpPost, Route("Add")]
        public new async Task<ActionResult> Add(SaveModel saveModel)
        {
            return await base.Add(saveModel);
        }
    }
}