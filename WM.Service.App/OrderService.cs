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
using WM.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace WM.Service.App
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderService : BaseSerivce<X.IRespository.DBSession.IWMDBSession>, IOrderService
    {
        private readonly X.IRespository.DBSession.IWMDBSession _ibll;
        private readonly ILogger<OrderService> _logger;
        private readonly IUserDomainService _userDomainService;
        private readonly IProductDomainService _productDomainService;
        public OrderService(X.IRespository.DBSession.IWMDBSession repository,
            IUserDomainService userDomainService, 
            IProductDomainService productDomainService,
            ILogger<OrderService> logger):base(repository)
        {
            _userDomainService = userDomainService;
            _productDomainService = productDomainService;
            _logger = logger;
        }
        ///////-----------------购物车----------------------
        /// <summary>
        /// 获取购物车信息
        /// </summary>
        /// <returns></returns>
        public ResultDto<OrderCradRP> GetOrderCarInfo(string uid)
        {
            var user = _userDomainService.GetUserByUID(uid);
            if (user == null) return Result<OrderCradRP>(ResponseCode.sys_token_invalid, "获取用户信息错误");
            var result = new OrderCradRP
            {
                TotalPrice = 0,
                OrderCradinfo = new List<OrderCradinfoRP> { },
            };
            var orderCard = _ibll.wm_order_card.Where(q => q.UID == uid && q.DataStatus == (byte)DataStatus.Enable).First();
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
        public ResultDto<bool> AddShoppingCar(string uid, AddShoppingCarRQ rq)
        {
            var user = _userDomainService.GetUserByUID(uid);
            if (user == null) return Result<bool>(ResponseCode.sys_token_invalid, "获取用户信息错误");
            var isSave = false;
            var product = _productDomainService.GetProductById(rq.ProductID);
            if (product == null)
            {
                return Result<bool>(ResponseCode.sys_param_format_error, "商品不存在");
            }
            var cardId = _ibll.wm_order_card.Where(q => q.UID == uid && q.DataStatus == (byte)DataStatus.Enable)
                                        .Select(a => a.ID).First();
            if (cardId == 0)
            {
                cardId = _ibll.wm_order_card.AddReturnId(new wm_order_card
                {
                    UID = uid,
                    Checked = false,
                    CreateTime = DateTime.Now,
                    DataStatus = (byte)DataStatus.Enable,
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
        public ResultDto<bool> ModifyShoppingCar(string uid, ModifyShoppingCarRQ rq)
        {
            var user = _userDomainService.GetUserByUID(uid);
            if (user == null) return Result<bool>(ResponseCode.sys_token_invalid, "获取用户信息错误");
            var isSave = false;
            var product = _productDomainService.GetProductById(rq.ProductID);
            if (product == null)
            {
                return Result<bool>(ResponseCode.sys_param_format_error, "商品不存在");
            }
            var cardId = _ibll.wm_order_card.Where(q => q.UID == uid && q.DataStatus == (byte)DataStatus.Enable)
                                        .Select(a => a.ID).First();
            if (cardId == 0)
            {
                cardId = _ibll.wm_order_card.AddReturnId(new wm_order_card
                {
                    UID = uid,
                    Checked = false,
                    CreateTime = DateTime.Now,
                    DataStatus = (byte)DataStatus.Enable,
                });
            }
            var cardinfo = _ibll.wm_order_card_info.Where(q => q.Order_CardID == cardId && q.ProductID == rq.ProductID && q.DataStatus == (byte)DataStatus.Enable).First();
            if (cardinfo != null)
            {

                cardinfo.Product_Num = rq.ProductNumber;

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
        public ResultDto<bool> RemoveShoppingCar(string uid, RemoveShoppingCarRQ rq)
        {
            var user = _userDomainService.GetUserByUID(uid);
            if (user == null) return Result<bool>(ResponseCode.sys_token_invalid, "获取用户信息错误");
            var isSave = false;
            var cardId = _ibll.wm_order_card.Where(q => q.UID == uid && q.DataStatus == (byte)DataStatus.Enable)
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
        
        ///////-----------------订单----------------------
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rq"></param>
        /// <returns></returns>
        public ResultDto<PageDto<OrderRP>> GetOrderPageList(string uid, OrderPageListRQ rq)
        {
            var user = _userDomainService.GetUserByUID(uid);
            if (user == null) return Result<PageDto<OrderRP>>(ResponseCode.sys_token_invalid, "获取用户信息错误");
            var result = new PageDto<OrderRP>(rq.pi, rq.ps) { lst = new List<OrderRP>() };

            // 查询条件
            var exp = PredicateBuilder.True<wm_order>();

            if (rq.OrderStatus > 0)
            {
                exp = exp.And(t => t.OrderStatus == rq.OrderStatus);
            }
            int totalCount = 0;
            var order = _ibll.wm_order.Where(q => q.UID == uid && q.DataStatus == (byte)DataStatus.Enable)
                                      .Where(exp)
                                      .OrderBy(q => q.CreateTime, OrderByType.Desc)
                                      .Select(q => new OrderRP
                                      {
                                          ID = q.ID,
                                          BillNo = q.BillNo,
                                          OrderStatus = q.OrderStatus,
                                      }).ToPageList(rq.pi, rq.ps, ref totalCount);
            result.pg.tc = totalCount;
            result.lst = order;

            result.lst.ForEach(q =>
            {
                q.OrderText = ((EnumOrderStatus)q.OrderStatus).Description();
                q.OrderInfos = _ibll.wm_order_info.Where(j => j.OrderId == q.ID)
                                                   .Select(j => new OrderInfoRQ
                                                   {
                                                       ProductID = j.ProductID,
                                                       ProductName = j.Product_Name,
                                                       ProductNumber = j.Product_Num,
                                                       ProductPrice = j.Product_Price,
                                                   }).ToList();
            });
            return Result(result);
        }
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rq"></param>
        /// <returns></returns>
        public ResultDto<bool> CreatePayOrder(string uid, CreateOrderRQ rq)
        {
            if (rq.AddressID <= 0) return Result<bool>(ResponseCode.sys_param_format_error, "请选择收货地址");
            if (rq.Products.Count == 0) return Result<bool>(ResponseCode.sys_param_format_error, "请选择商品");
            if(rq.CustomerMessage.Length>50) return Result<bool>(ResponseCode.sys_param_format_error, "留言过长,请重新输入");
            var user = _userDomainService.GetUserByUID(uid);
            if (user == null) return Result<bool>(ResponseCode.sys_token_invalid, "获取用户信息错误");

            var addr = _ibll.wm_user_shopping_address.Where(q => q.ID == rq.AddressID && q.UID == uid).First();
            if (addr == null) return Result<bool>(ResponseCode.sys_param_format_error, "收货地址错误！请重新提交");

            var city = _ibll.cm_city.Where(q => q.ID == addr.CityID).Select(q => q.Name).First();

            var province = _ibll.cm_province.Where(q => q.ID == addr.ProvinceID).Select(q => q.Name).First();
            var a = string.Empty;
            if (city == province) a = city;
            else a = $"{province}{city}";
            addr.Receiver_Address = $"{a}{addr.Receiver_Address}";//拼接省市区
            // 生成内部订单号
            var billNo = $"SH{DateTime.Now.Ticks}";
            var order = new wm_order
            {
                BillNo=billNo,
                DataStatus=(byte)DataStatus.Enable,
                PayStatus= (byte)EnumPayStatus.WaitPayment,
                WxOrderNo="",
                UID=user.UID,
                CreateTime=DateTime.Now,
                Distribution=1,
                Receiver_Address=addr.Receiver_Address,
                Receiver_Name=addr.Receiver_Name,
                Receiver_Phone=addr.Receiver_Phone,
                OrderStatus= (byte)EnumOrderStatus.WaitPayment,
                CustomerMessage=rq.CustomerMessage,
                OrderPrice=0,
            };
            var orderinfoList = new List<wm_order_info>();
            foreach (var item in rq.Products)
            {
               var product=_ibll.wm_product.Where(q => q.DataStatus == (byte)DataStatus.Enable && q.ID == item.ProductID).First();
                if (product != null)
                {
                    orderinfoList.Add(new wm_order_info
                    {
                        OrderId = 0,
                        CreateTime = DateTime.Now,
                        DataStauts = (byte)DataStatus.Enable,
                        ProductID = item.ProductID,
                        Product_Icon= product.Icon,
                        Product_Name= product.Name,
                        Product_Num=item.ProductNumber,
                        Product_Price=product.Price,
                    });
                }
            }
            if(orderinfoList.Count==0) return Result<bool>(ResponseCode.sys_param_format_error, "选择的商品无效,请重新选购!");
            order.OrderPrice=orderinfoList.Sum(q => q.Product_Price);
            var orderCarId = 0;
            if (rq.IsEmptyShoppingCar)
            {
                orderCarId = _ibll.wm_order_card.Where(q => q.DataStatus == (byte)DataStatus.Enable && q.UID == q.UID)
                                                .Select(q=>q.ID)
                                                .First();
            }
            try
            {
                _ibll.DB.Ado.BeginTran();
                //创建订单
               var orderId= _ibll.wm_order.AddReturnId(order);

                orderinfoList.ForEach(q => q.OrderId = orderId);

                _ibll.wm_order_info.AddRange(orderinfoList);

                if (rq.IsEmptyShoppingCar) { 
                
                }
                //清除购物车
                if (rq.IsEmptyShoppingCar&&orderCarId>0)
                {
                    var tranSql = $"update {nameof(wm_order_card_info)} SET DataStaus={(byte)DataStatus.Delete} where DataStaus={(byte)DataStatus.Enable} and Order_CardID={orderCarId}";

                    _ibll.Sql_ExecuteCommand(tranSql);
                }
                _ibll.DB.Ado.CommitTran();

              
            }
            catch (System.Exception ex)
            {
                _ibll.DB.Ado.RollbackTran();
                _logger.LogError(ex, "下单失败!");
                return Result(false);
            }
            return Result(true);
        }
        /// <summary>
        /// 订单详情
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rq"></param>
        /// <returns></returns>
        public ResultDto<OrderinfoRP> GetOrderInfo(string uid, OrderRQ rq) {

            var result = new OrderinfoRP { };
            var order = _ibll.wm_order.Where(q => q.BillNo == rq.BillNo&&q.UID== uid).First();
            if (order == null) return Result<OrderinfoRP>(ResponseCode.sys_token_invalid, "未查到订单!");
            if (order != null)
            {

                result.OrderText = ((EnumOrderStatus)order.OrderStatus).Description();
                result.BillNo = order.BillNo;
                result.ID = order.ID;
                result.Receiver_Address = order.Receiver_Address;
                result.Receiver_Name = order.Receiver_Name;
                result.Receiver_Phone = order.Receiver_Phone;
                result.CreateTime = order.CreateTime;
                result.OrderInfos = _ibll.wm_order_info.Where(j => j.OrderId == order.ID)
                                                   .Select(j => new OrderInfoRQ
                                                   {
                                                       ProductID = j.ProductID,
                                                       ProductName = j.Product_Name,
                                                       ProductNumber = j.Product_Num,
                                                       ProductPrice = j.Product_Price,
                                                   }).ToList();
            }
            return Result(result);
            
        }
    }
}
