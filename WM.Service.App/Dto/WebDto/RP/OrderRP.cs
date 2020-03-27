using System;
using System.Collections.Generic;
using System.Text;

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
 
}
