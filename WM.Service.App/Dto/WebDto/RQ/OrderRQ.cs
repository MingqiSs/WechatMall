using System;
using System.Collections.Generic;
using System.Text;

namespace WM.Service.App.Dto.WebDto.RQ
{
    /// <summary>
    /// 
    /// </summary>
    public class AddShoppingCarRQ: OrderProductRQ
    {
       
    }
    /// <summary>
    /// 
    /// </summary>
    public class ModifyShoppingCarRQ: OrderProductRQ
    {
    }
    /// <summary>
    /// 
    /// </summary>
    public class RemoveShoppingCarRQ {
        /// <summary>
        /// 商品列表
        /// </summary>
        public List<string> ProductIDs { get; set; }
    }

    public class OrderPageListRQ : PageRQ
    {
        /// <summary>
        /// 订单状态 全部
        /// </summary>
        public int OrderStatus { get; set; }

    }

    public class CreateOrderRQ
    {

        /// <summary>
        /// 用户地址id
        /// </summary>
        public int AddressID { get; set; }
        /// <summary>
        /// 客户留言
        /// </summary>
        public string CustomerMessage { get; set; } = string.Empty;
        /// <summary>
        /// 商品
        /// </summary>
        public List<OrderProductRQ> Products { get; set; }
        /// <summary>
        /// 是否清空购物车(从购物车下单需要传true)
        /// </summary>
        public bool IsEmptyShoppingCar { get; set; } 
    }
    public class OrderProductRQ
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public string ProductID { get; set; }

        /// <summary>
        /// 商品数量 默认一个
        /// </summary>
        public int ProductNumber { get; set; } = 1;
    }
    /// <summary>
    /// 
    /// </summary>
    public class OrderInfoRQ
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public string ProductID { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int ProductNumber { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal ProductPrice { get; set; }
    }
    public class OrderRQ
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string BillNo { get; set; }

    }
}
