using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WM.Infrastructure.Models
{
    public enum EnumModuleType : byte
    {
        /// <summary>
        /// 全部
        /// </summary>
        [Description("全部")]
        All = 0,
        /// <summary>
        /// 首页横幅
        /// </summary>
        [Description("首页横幅")]
        HomeBanner = 1,
        /// <summary>
        /// 新手专区
        /// </summary>
        [Description("新手专区")]
        NoviceArea = 2,
        /// <summary>
        /// 最新福利
        /// </summary>
        [Description("最新福利")]
        LatestBenefits = 3,
        /// <summary>
        /// 最新活动
        /// </summary>
        [Description("最新活动")]
        LatestActivities = 4,
        /// <summary>
        /// 首页弹窗(新入金活动)
        /// </summary>
        [Description("首页弹窗(新入金活动)")]
        HomeWindowNewGold = 5,
        /// <summary>
        /// 首页弹窗(模似交易)
        /// </summary>
        [Description("首页弹窗(模似交易)")]
        HomeWindowAnalogTransaction = 6
    }
}
