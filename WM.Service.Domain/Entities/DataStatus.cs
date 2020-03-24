using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WM.Service.Domain.Entities
{
    public enum DataStatus : byte
    {
        /// <summary>
        /// 全部
        /// </summary>
        [Description("全部")]
        All = 0,
        /// <summary>
        /// 启用
        /// </summary>
        [Description("启用")]
        Enable = 1,
        /// <summary>
        /// 停用
        /// </summary>
        [Description("停用")]
        Disable = 2,
        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Delete = 3

    }
}
