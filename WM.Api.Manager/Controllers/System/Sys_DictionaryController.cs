﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WM.Infrastructure.Controllers.Basic;
using WM.Infrastructure.Filters;
using WM.Service.App.Interface.System;


namespace WM.Api.Manager.Controllers.System
{
    [Route("api/[controller]")]
    [ApiController]
    public class Sys_DictionaryController : ApiBaseController<ISys_DictionaryService>
    {
        /// <summary>
        /// 字典
        /// </summary>
        /// <param name="service"></param>
        public Sys_DictionaryController(ISys_DictionaryService service)
               : base(service)
        {

        }
        /// <summary>
        /// 获取字典值
        /// </summary>
        /// <param name="dicNos"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVueDictionary")]
        [ApiActionPermission()]
        public async Task<IActionResult> GetVueDictionary([FromBody]string[] dicNos)
        {
            return Ok(await Service.GetVueDictionary(dicNos));
        }
    }
}