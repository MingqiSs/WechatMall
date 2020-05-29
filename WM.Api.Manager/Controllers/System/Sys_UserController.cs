using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WM.Infrastructure.Controllers.Basic;
using WM.Service.Domain.Interface;

namespace WM.Api.Manager.Controllers.System
{
    [Route("api/[controller]")]
    [ApiController]
    public class Sys_UserController : ApiBaseController<ISys_UserDoaminService>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public Sys_UserController(ISys_UserDoaminService service)
                 : base(service)
        {

        }
    }
}