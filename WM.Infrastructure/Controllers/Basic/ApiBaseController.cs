using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WM.Infrastructure.Filters;
using WM.Infrastructure.Models;

namespace WM.Infrastructure.Controllers.Basic
{
    [Authorize]
    [ApiController]
    public class ApiBaseController<IServiceBase> : BaseController<IServiceBase>
    {
        public ApiBaseController(IServiceBase service)
      : base(service)
        {
        }
        public ApiBaseController(string projectName, string folder, string tablename, IServiceBase service)
       : base(projectName, folder, tablename, service)
        {
        }
        /// <summary>
        /// 通过key删除数据
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        [ApiActionPermission(Enums.ActionPermissionOptions.Delete)]
        [HttpPost, Route("Del")]
        public new async Task<ActionResult> Del([FromBody] object[]  keys)
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
        [ApiActionPermission(Enums.ActionPermissionOptions.Search)]
        [HttpPost, Route("GetPageData")]
        public new async Task<ActionResult> GetPageData( PageDataOptions loadData)
        {
            return await base.GetPageData(loadData);
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="loadData"></param>
        /// <returns></returns>
        [ApiActionPermission(Enums.ActionPermissionOptions.Update)]
        [HttpPost, Route("Update")]
        public new async Task<ActionResult> Update(SaveModel saveModel)
        {
            return await base.Update(saveModel);
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="loadData"></param>
        /// <returns></returns>
        [ApiActionPermission(Enums.ActionPermissionOptions.Update)]
        [HttpPost, Route("Add")]
        public new async Task<ActionResult> Add(SaveModel saveModel)
        {
            return await base.Add(saveModel);
        }
    }
}
