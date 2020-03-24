using System;
using System.Collections.Generic;
using System.Text;

namespace WM.Infrastructure.Models
{
    public class ReturnModel
    {
        /// <summary>
        /// 1-成功，0-失败
        /// </summary>
        public int ok { get; set; }
        /// <summary>
        /// 通讯状态
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 返回内容主体
        /// </summary>
        public object data { get; set; }
        /// <summary>
        /// 接口执行耗时
        /// </summary>
        public string Timespan { get; set; }
    }
}
