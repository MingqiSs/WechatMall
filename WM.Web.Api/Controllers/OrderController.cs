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
    [Authorize]
    [ApiController]
    [Route("v1")]
    public class OrderController : ControllerBase
    {
       
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        /// <summary>
        /// 查看购物车
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("M301")]
        [ProducesResponseType(typeof(ResultDto<OrderCradRP>), 200)]
        public IActionResult GetOrderCardInfo()
        {
            var r=_orderService.GetOrderCardInfo(User.GetToken().UID);
            return Ok(r);
        }
        /// <summary>
        ///商品添加到购物车
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("M302")]
        [ProducesResponseType(typeof(ResultDto<object>), 200)]
        public IActionResult AddOrderCard()
        {
            return Ok();
        }
        /// <summary>
        ///商品移除购物车
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("M303")]
        [ProducesResponseType(typeof(ResultDto<object>), 200)]
        public IActionResult RemoveOrderCard()
        {
            return Ok();
        }
        /// <summary>
        ///订单列表
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("M304")]
        [ProducesResponseType(typeof(ResultDto<object>), 200)]
        public IActionResult GetOrderPageList()
        {
            return Ok();
        }
        /// <summary>
        ///创建订单
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("M305")]
        [ProducesResponseType(typeof(ResultDto<object>), 200)]
        public IActionResult CreateOrder()
        {
            return Ok();
        }
        /// <summary>
        ///删除订单
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("M306")]
        [ProducesResponseType(typeof(ResultDto<object>), 200)]
        public IActionResult RemoveOrder()
        {
            return Ok();
        }
    }
}
