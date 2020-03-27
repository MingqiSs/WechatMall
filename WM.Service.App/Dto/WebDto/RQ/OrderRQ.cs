using System;
using System.Collections.Generic;
using System.Text;

namespace WM.Service.App.Dto.WebDto.RQ
{
    /// <summary>
    /// 
    /// </summary>
    public class ModifyOrderCardRQ
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
}
