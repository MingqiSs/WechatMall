using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
        //[ApiActionPermission(Enums.ActionPermissionOptions.Delete)]
        [HttpPost, Route("Del")]
        public new async Task<ActionResult> Del( KeyOptions keys)
        {
            return await base.Del(keys);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="loadData"></param>
        /// <returns></returns>
        //    [ApiActionPermission(Enums.ActionPermissionOptions.Search)]
        [HttpPost, Route("GetPageData")]
        public new async Task<ActionResult> GetPageData( PageDataOptions loadData)
        {
            return await base.GetPageData(loadData);
        }
    }
}
