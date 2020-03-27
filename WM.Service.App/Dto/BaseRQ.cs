using System;
using System.Collections.Generic;
using System.Text;

namespace WM.Service.App.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class PageRQ
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int pi { get; set; }
        /// <summary>
        /// 每页显示多少条
        /// </summary>
        public int ps { get; set; }
    }
}
