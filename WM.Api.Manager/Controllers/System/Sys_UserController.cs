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
        private readonly TokenManagement _tokenManagement;
     /// <summary>
     /// 
     /// </summary>
     /// <param name="service"></param>
     /// <param name="tokenManagement"></param>
        public Sys_UserController(ISys_UserService service,IOptions<TokenManagement> tokenManagement)
                 : base(service)
        {
            _tokenManagement = tokenManagement.Value;
        }
        /// <summary>
        ///登录
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("login"), AllowAnonymous]
        public IActionResult Login(M_AdminUserRQ rq)
        {
            var r = Service.Login(rq.UserName, rq.Password);
            if (r.Status)
            {
                var data = (M_AdminUserRP)r.Data;
                var adminToken = new AdminUser
                {
                    User_Id = data.id,
                    Email = data.Email,
                    Role_Id = data.RoleId,
                    UserName = data.Name,
                };
                var token = TokenHelper.CreateAdminToken(_tokenManagement, adminToken);
                Response.Headers.Add("Authorization", new StringValues(token));
            }
            return Ok(r);
        }
    }
}