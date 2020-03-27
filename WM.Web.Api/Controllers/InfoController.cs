using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WM.Infrastructure.Config;
using WM.Infrastructure.Models;
using WM.Service.App.Dto.WebDto.RP;
using WM.Service.App.Dto.WebDto.RQ;
using WM.Service.App.Interface;
using WM.Web.Api.Configurations;

namespace WM.Web.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class InfoController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IInfoService _infoService;
        public InfoController(ILogger<UserController> logger, IInfoService infoService)
        {
            _logger = logger;
            _infoService = infoService;
        }
        /// <summary>
        /// 获取地区列表(省市)
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost, Route("M201")]
        [ProducesResponseType(typeof(ResultDto<UserLoginRP>), 200)]
        public IActionResult GetDistrictList()
        {
          var r=  _infoService.GetDistrictList();
            return Ok(r);
        }
      
    }
}
