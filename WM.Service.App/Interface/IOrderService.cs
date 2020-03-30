using System;
using System.Collections.Generic;
using System.Text;
using WM.Infrastructure.Models;
using WM.Service.App.Dto.WebDto.RP;
using WM.Service.App.Dto.WebDto.RQ;

namespace WM.Service.App.Interface
{
   public interface IOrderService
    {
        /// <summary>
        /// 查看购物车信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        ResultDto<OrderCradRP> GetOrderCarInfo(string uid);
        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rq"></param>
        /// <returns></returns>
        ResultDto<bool> AddShoppingCar(string uid, AddShoppingCarRQ rq);
        /// <summary>
        /// 修改购物车商品数量
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rq"></param>
        /// <returns></returns>
        ResultDto<bool> ModifyShoppingCar(string uid, ModifyShoppingCarRQ rq);
        /// <summary>
        /// 商品移除购物车
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rq"></param>
        /// <returns></returns>
        ResultDto<bool> RemoveShoppingCar(string uid, RemoveShoppingCarRQ rq);
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rq"></param>
        /// <returns></returns>
        ResultDto<PageDto<OrderRP>> GetOrderPageList(string uid, OrderPageListRQ rq);
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rq"></param>
        /// <returns></returns>
        ResultDto<bool> CreatePayOrder(string uid, CreateOrderRQ rq);
        /// <summary>
        /// 查看订单
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rq"></param>
        /// <returns></returns>
        ResultDto<OrderinfoRP> GetOrderInfo(string uid, OrderRQ rq);
    }
}
