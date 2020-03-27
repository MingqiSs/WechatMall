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
    public class ProductController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        /// <summary>
        /// 获取商品列表(带分页)
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("M201")]
        [ProducesResponseType(typeof(ResultDto<PageDto<ProductRP>>), 200)]
        public IActionResult GetproductPageList(ProductRQ rq)
        {
            var r = _productService.GetProductPageList(rq);
            return Ok(r);
        }
        /// <summary>
        /// 获取商品分类
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("M202")]
        [ProducesResponseType(typeof(ResultDto<List<ProductTypeRP>>), 200)]
        public IActionResult GetProductTypeList()
        {
            var r = _productService.GetProductTypeList();
            return Ok(r);
        }
        /// <summary>
        /// 获取商品标签
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("M203")]
        [ProducesResponseType(typeof(ResultDto<List<ProductTagRP>>), 200)]
        public IActionResult GetProductTag()
        {
            var r = _productService.GetProductTagList();
            return Ok(r);
        }
    }
}
