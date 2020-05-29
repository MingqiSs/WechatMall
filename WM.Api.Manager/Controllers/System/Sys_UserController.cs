using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MC.Infrastructure.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WM.Infrastructure.Config;
using WM.Infrastructure.Controllers.Basic;
using WM.Infrastructure.Models;
using WM.Service.App.Dto.ManagerDto.RQ;
using WM.Service.App.Interface;
using WM.Service.Domain.Interface;

namespace WM.Api.Manager.Controllers.System
{
    [Route("api/[controller]")]
    [ApiController]
    public class Sys_UserController : ApiBaseController<ISys_UserDoaminService>
    {
        private readonly Sys_UserService _managerService;
        private readonly TokenManagement _tokenManagement;
       /// <summary>
       /// 
       /// </summary>
       /// <param name="service"></param>
       /// <param name="managerService"></param>
       /// <param name="tokenManagement"></param>
        public Sys_UserController(ISys_UserDoaminService service, Sys_UserService managerService, TokenManagement tokenManagement)
                 : base(service)
        {
            _managerService = managerService;
            _tokenManagement = tokenManagement;
        }
        /// <summary>
        ///登录
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("Login"), AllowAnonymous]
        public IActionResult Login(M_AdminUserRQ rq)
        {
            var r = _managerService.AdminLogin(rq.UserName, rq.Password, "");
            if (r.Res == 1)
            {
                var adminToken = new AdminToken
                {
                    Id = r.Dt.id,
                    Email = r.Dt.Email,
                    Mobile = r.Dt.Moblie,
                    Role = r.Dt.RoleId,
                    RoleCode = r.Dt.RoleCode,
                    Name = r.Dt.Name,
                };
                var token = TokenHelper.CreateAdminToken(_tokenManagement, adminToken);
                Response.Headers.Add("Authorization", new StringValues(token));
            }
            return Ok(r);
        }
    }
}