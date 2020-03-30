using System;
using System.Collections.Generic;
using System.Text;

namespace WM.Service.App.Dto.WebDto.RQ
{
    /// <summary>
    /// 
    /// </summary>
    public class AddShoppingCardRQ
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
    public class ModifyShoppingCardRQ
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public string ProductID { get; set; }

        /// <summary>
        /// 商品数量 默认一个
        /// </summary>
        public int Product_Num { get; set; } = 1;
    }
    /// <summary>
    /// 
    /// </summary>
    public class RemoveShoppingCardRQ {
        /// <summary>
        /// 商品列表
        /// </summary>
        public List<string> ProductIDs { get; set; }
    }
}
