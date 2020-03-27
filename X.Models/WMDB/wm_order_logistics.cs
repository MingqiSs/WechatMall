using System;
using System.Linq;
using System.Text;

namespace X.Models.WMDB
{
    [Serializable]
    public partial class wm_order_logistics
    {
           public wm_order_logistics(){}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public string OrderID { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public int? Logistics_CompanyID { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public string Logistics_CompanyCode { get; set; }

           /// <summary>
           /// Desc:快递单号
           /// Default:
           /// Nullable:True
           /// </summary>
           public string TrackingNumber { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public DateTime CreateTime { get; set; }

           /// <summary>
           /// Desc:发货时间
           /// Default:
           /// Nullable:True
           /// </summary>
           public DateTime? DeliveryTime { get; set; }

           /// <summary>
           /// Desc:收货时间
           /// Default:
           /// Nullable:True
           /// </summary>
           public DateTime? ReceivingTime { get; set; }


    }
}
