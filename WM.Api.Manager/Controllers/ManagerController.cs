using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MC.Infrastructure.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using WM.Infrastructure.Config;
using WM.Infrastructure.Models;
using WM.Service.App.Dto.ManagerDto.RQ;
using WM.Service.App.Interface;

namespace WM.Api.Manager.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v1")]
    public class ManagerController : ControllerBase
    {
        private readonly ILogger<ManagerController> _logger;
        private readonly IManagerService  _managerService;
        private readonly TokenManagement _tokenManagement;
        public ManagerController(ILogger<ManagerController> logger, IManagerService managerService, IOptions<TokenManagement> tokenManagement)
        {
            _logger = logger;
            _managerService = managerService;
            _tokenManagement = tokenManagement.Value;
        }
        /// <summary>
        ///登录
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost, Route("M101")]
        [ProducesResponseType(typeof(ResultDto<bool>), 200)]
        public IActionResult Login(M_AdminUserRQ rq)
        {
            var r = _managerService.AdminLogin(rq.UserName, rq.Password,"");
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
        /// <summary>
        ///查看用户资料
        /// </summary>
        /// <returns></returns>
    
        [HttpPost, Route("M102")]
        [ProducesResponseType(typeof(ResultDto<bool>), 200)]
        public IActionResult GetUserInfo()
        {
           var uid= User.GetToken().UID;
            return Ok(uid);
        }
    }
}
