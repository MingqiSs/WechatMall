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
using WM.Service.Domain.Entities;

namespace WM.Service.App
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderService : BaseSerivce, IOrderService
    {
        private readonly X.IRespository.DBSession.IWMDBSession _ibll;
        private readonly IUserDomainService _userDomainService;
        private readonly IProductDomainService _productDomainService;
        public OrderService(X.IRespository.DBSession.IWMDBSession ibll, IUserDomainService userDomainService, IProductDomainService productDomainService)
        {
            _ibll = ibll;
            _userDomainService = userDomainService;
            _productDomainService = productDomainService;
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
            var orderCard= _ibll.wm_order_card.Where(q => q.UID == uid&&q.DataStauts==(byte)DataStatus.Enable).First();
            if (orderCard != null)
            {
                var order = _ibll.DB.Queryable<wm_order_card_info, wm_product>((c, p) => new object[] {
                                            JoinType.Inner,c.ProductID==p.ID,
                                      })
                            .Where((c, p) => c.Order_CardID == orderCard.ID
                                    && c.DataStatus == (byte)DataStatus.Enable
                                    && p.DataStatus == (byte)DataStatus.Enable)
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
        /// <summary>
        /// 添加商品到购物车
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rq"></param>
        /// <returns></returns>
        public ResultDto<bool> AddShoppingCard(string uid, AddShoppingCardRQ rq)
        {
            var user = _userDomainService.GetUserByUID(uid);
            if (user == null) return Result<bool>(ResponseCode.sys_token_invalid, "获取用户信息错误");
            var isSave = false;
            var product=_productDomainService.GetProductById(rq.ProductID);
            if (product == null)
            {
                return Result<bool>(ResponseCode.sys_param_format_error, "商品不存在");
            }
           var cardId = _ibll.wm_order_card.Where(q => q.UID == uid && q.DataStauts == (byte)DataStatus.Enable)
                                       .Select(a=>a.ID).First();
            if (cardId == 0)
            {
                cardId = _ibll.wm_order_card.AddReturnId(new wm_order_card
                {
                    UID=uid,
                    Checked=false,
                    CreateTime=DateTime.Now,
                    DataStauts=(byte)DataStatus.Enable,
                });
            }
            isSave = _ibll.wm_order_card_info.Add(new wm_order_card_info
            {
                Order_CardID = cardId,
                DataStatus = (byte)DataStatus.Enable,
                ProductID = product.ID,
                Product_Num = rq.ProductNumber,
                CreateTime = DateTime.Now,
            });
            return Result(isSave);
        }

        /// <summary>
        /// 修改商品到购物车
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rq"></param>
        /// <returns></returns>
        public ResultDto<bool> ModifyShoppingCard(string uid, ModifyShoppingCardRQ rq)
        {
            var user = _userDomainService.GetUserByUID(uid);
            if (user == null) return Result<bool>(ResponseCode.sys_token_invalid, "获取用户信息错误");
            var isSave = false;
            var product = _productDomainService.GetProductById(rq.ProductID);
            if (product == null)
            {
                return Result<bool>(ResponseCode.sys_param_format_error, "商品不存在");
            }
            var cardId = _ibll.wm_order_card.Where(q => q.UID == uid && q.DataStauts == (byte)DataStatus.Enable)
                                        .Select(a => a.ID).First();
            if (cardId == 0)
            {
                cardId = _ibll.wm_order_card.AddReturnId(new wm_order_card
                {
                    UID = uid,
                    Checked = false,
                    CreateTime = DateTime.Now,
                    DataStauts = (byte)DataStatus.Enable,
                });
            }
            var cardinfo = _ibll.wm_order_card_info.Where(q => q.Order_CardID == cardId && q.ProductID == rq.ProductID && q.DataStatus == (byte)DataStatus.Enable).First();
            if (cardinfo != null)
            {

                cardinfo.Product_Num = rq.Product_Num;

                isSave = _ibll.wm_order_card_info.Update(cardinfo);
            }
            return Result(isSave);
        }
        /// <summary>
        /// 修改商品到购物车
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rq"></param>
        /// <returns></returns>
        public ResultDto<bool> RemoveShoppingCard(string uid, RemoveShoppingCardRQ rq)
        {
            var user = _userDomainService.GetUserByUID(uid);
            if (user == null) return Result<bool>(ResponseCode.sys_token_invalid, "获取用户信息错误");
            var isSave = false;
            var cardId = _ibll.wm_order_card.Where(q => q.UID == uid && q.DataStauts == (byte)DataStatus.Enable)
                                        .Select(a => a.ID).First();
            if (cardId == 0)
            {
                return Result<bool>(ResponseCode.sys_param_format_error, "购物车没有商品");
            }
            foreach (var productID in rq.ProductIDs)
            {
                var cardinfo = _ibll.wm_order_card_info.Where(q => q.Order_CardID == cardId && q.ProductID == productID && q.DataStatus == (byte)DataStatus.Enable).First();
                if (cardinfo != null)
                {
                    cardinfo.DataStatus = (byte)DataStatus.Delete;
                    isSave = _ibll.wm_order_card_info.Update(cardinfo);
                }
            }
            return Result(isSave);
        }
    }
}
