using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace X.Models.WMDB
{
    [Serializable]
    public partial class wm_order_logistics_flow
    {
           public wm_order_logistics_flow(){}

           /// <summary>
           /// Desc:订单标号
           /// Default:
           /// Nullable:False
           /// </summary>
           public string TrackingNumber { get; set; }

           /// <summary>
           /// Desc:物流公司标号
           /// Default:
           /// Nullable:False
           /// </summary>
           public int Logistics_CompanyID { get; set; }

           /// <summary>
           /// Desc:订单号
           /// Default:
           /// Nullable:False
           /// </summary>
           public string OrederID { get; set; }

           /// <summary>
           /// Desc:根据第三方接口返回来的结果信息
           /// Default:
           /// Nullable:False
           /// </summary>
           public string Remark { get; set; }


    }
}
