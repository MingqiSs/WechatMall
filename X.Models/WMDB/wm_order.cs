using System;
using System.Linq;
using System.Text;

namespace X.Models.WMDB
{
    [Serializable]
    public partial class wm_order
    {
           public wm_order(){}

           /// <summary>
           /// Desc:订单状态
           /// Default:
           /// Nullable:True
           /// </summary>
           public byte? Oredstatus { get; set; }

           /// <summary>
           /// Desc:(1 在线支付,2货到付款)
           /// Default:1
           /// Nullable:True
           /// </summary>
           public byte? Distribution { get; set; }

           /// <summary>
           /// Desc:订单价格
           /// Default:
           /// Nullable:True
           /// </summary>
           public decimal? OrderPrice { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public DateTime CreateTime { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public int ID { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public DateTime? ModifyTime { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public string UID { get; set; }

           /// <summary>
           /// Desc:收货人姓名
           /// Default:
           /// Nullable:True
           /// </summary>
           public string Receiver_Name { get; set; }

           /// <summary>
           /// Desc:账单号
           /// Default:
           /// Nullable:True
           /// </summary>
           public string BillNo { get; set; }

           /// <summary>
           /// Desc:收货人电话
           /// Default:
           /// Nullable:True
           /// </summary>
           public string Receiver_Phone { get; set; }

           /// <summary>
           /// Desc:微信订单号
           /// Default:
           /// Nullable:True
           /// </summary>
           public string WxOrderNo { get; set; }

           /// <summary>
           /// Desc:收货人地址
           /// Default:
           /// Nullable:True
           /// </summary>
           public string Receiver_Address { get; set; }


    }
}
