using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MC.Infrastructure.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using WM.Api.Manager.Controllers.Basic;
using WM.Infrastructure.Config;
using WM.Infrastructure.Controllers.Basic;
using WM.Infrastructure.Extensions.AutofacManager;
using WM.Infrastructure.Models;
using WM.Service.App.Dto.ManagerDto.RP;
using WM.Service.App.Dto.ManagerDto.RQ;
using WM.Service.App.Interface;
using WM.Service.Domain.Interface;

namespace WM.Api.Manager.Controllers.System
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class Sys_UserController : ApiBaseController<ISys_UserService>
    {
      
     /// <summary>
     /// 
     /// </summary>
     /// <param name="service"></param>
        public Sys_UserController(ISys_UserService service)
                 : base(service)
        {
          
        }
        /// <summary>
        ///登录
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("login"), AllowAnonymous]
        public IActionResult Login(M_AdminUserRQ rq)
        {
            var r = Service.Login(rq.UserName, rq.Password);
           
            return Ok(r);
        }
    }
}