using System;
using System.Collections.Generic;
using System.Text;

namespace WM.Service.App.Dto.WebDto.RQ
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductRQ : PageRQ
    {
        /// <summary>
        /// 关键字搜索
        /// </summary>
        public string Keywords { get; set; }
        /// <summary>
        /// 标签id 默认传0
        /// </summary>
        public int ProductTagID { get; set; }
        /// <summary>
        /// 类型id 默认传0
        /// </summary>
        public int ProductTypeID { get; set; }
    }
}
