using System;
using System.Collections.Generic;
using System.Text;
using WM.Service.App.Dto.WebDto.RQ;

namespace WM.Service.App.Dto.WebDto.RP
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderCradRP
    {
        /// <summary>
        /// 总价
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// 购物车商品列表
        /// </summary>
        public List<OrderCradinfoRP> OrderCradinfo { get; set; }
    }
    public class OrderCradinfoRP
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public string ProductID { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int Product_Num { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal Product_Price { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class OrderRP
    {
        /// <summary>
        /// 订单id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string BillNo { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public byte  OrderStatus { get; set; }
        /// <summary>
        /// 订单状态文本
        /// </summary>
        public string OrderText { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<OrderInfoRQ> OrderInfos { get; set; }
    }

    public class OrderinfoRP: OrderRP
    {
        /// <summary>
        /// 收货人姓名
        /// </summary>
       public string Receiver_Name { get; set; }
        /// <summary>
        /// 收货人电话
        /// </summary>
        public string Receiver_Phone { get; set; }
        /// <summary>
        /// 收货人地址
        /// </summary>
        public string Receiver_Address { get; set; }
        /// <summary>
        /// 订单创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

    }
}
