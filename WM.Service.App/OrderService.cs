using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using WM.Infrastructure.Models;
using WM.Service.App.Dto.WebDto.RP;
using WM.Service.App.Interface;
using WM.Service.Domain.Interface;
using X.Models.WMDB;
using System.Linq;
using WM.Service.App.Dto.WebDto.RQ;

namespace WM.Service.App
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderService : BaseSerivce, IOrderService
    {
        private readonly X.IRespository.DBSession.IWMDBSession _ibll;
        private readonly IUserDomainService _userDomainService;
        public OrderService(X.IRespository.DBSession.IWMDBSession ibll, IUserDomainService userDomainService)
        {
            _ibll = ibll;
            _userDomainService = userDomainService;
        }
        /// <summary>
        /// 获取购物车信息
        /// </summary>
        /// <returns></returns>
        public ResultDto<OrderCradRP> GetOrderCardInfo(string uid)
        {
            var user = _userDomainService.GetUserByUID(uid);
            if (user == null) return Result<OrderCradRP>(ResponseCode.sys_token_invalid, "获取用户信息错误");
            var result = new OrderCradRP
            {
                TotalPrice = 0,
                OrderCradinfo = new List<OrderCradinfoRP> { },
            };
            var orderCard= _ibll.wm_order_card.Where(q => q.UID == uid&&q.DataStauts==(byte)EnumDataStatus.Enable).First();
            if (orderCard != null)
            {
                var order = _ibll.DB.Queryable<wm_order_card_info, wm_product>((c, p) => new object[] {
                                            JoinType.Inner,c.ProductID==p.ID,
                                      })
                            .Where((c, p) => c.Order_CardID == orderCard.ID
                                    && c.DataStatus == (byte)EnumDataStatus.Enable
                                    && p.DataStatus == (byte)EnumDataStatus.Enable)
                            .Select((c, p) => new OrderCradinfoRP
                            {
                                ProductID = c.ProductID,
                                ProductName = p.Name,
                                Product_Num = c.Product_Num,
                                Product_Price = p.Price
                            }).ToList();
                result.OrderCradinfo = order;
                result.TotalPrice = order.Sum(q => q.Product_Price * q.Product_Num);
            }
           
            return Result(result);
        }
        ///// <summary>
        ///// 修改商品到购物车
        ///// </summary>
        ///// <param name="uid"></param>
        ///// <param name="rq"></param>
        ///// <returns></returns>
        //public ResultDto<bool> ModifyOrderCard(string uid, ModifyOrderCardRQ rq)
        //{
        //    var user = _userDomainService.GetUserByUID(uid);
        //    if (user == null) return Result<OrderCradRP>(ResponseCode.sys_token_invalid, "获取用户信息错误");

        //}
    }
}
