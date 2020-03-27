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
    [Route("v1")]
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
        [HttpPost, Route("M201")]
        [ProducesResponseType(typeof(ResultDto<UserLoginRP>), 200)]
        public IActionResult GetDistrictList()
        {
          var r=  _infoService.GetDistrictList();
            return Ok(r);
        }
        /// <summary>
        /// 获取商品列表(带分页)
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("M202")]
        [ProducesResponseType(typeof(ResultDto<PageDto<ProductRP>>), 200)]
        public IActionResult GetproductPageList(ProductRQ rq)
        {
           var r= _infoService.GetProductPageList(rq);
            return Ok(r);
        }
        /// <summary>
        /// 获取商品分类
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("M203")]
        [ProducesResponseType(typeof(ResultDto<List<ProductTypeRP>>), 200)]
        public IActionResult GetProductTypeList()
        {
            var r = _infoService.GetProductTypeList();
            return Ok(r);
        }
        /// <summary>
        /// 获取商品标签
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("M204")]
        [ProducesResponseType(typeof(ResultDto<List<ProductTagRP>>), 200)]
        public IActionResult GetProductTag()
        {
            var r = _infoService.GetProductTagList();
            return Ok(r);
        }
    }
}
