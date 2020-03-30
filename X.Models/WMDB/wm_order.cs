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
           /// Desc:订单价格
           /// Default:
           /// Nullable:False
           /// </summary>
           public decimal OrderPrice { get; set; }

           /// <summary>
           /// Desc:数据状态
           /// Default:1
           /// Nullable:False
           /// </summary>
           public byte DataStatus { get; set; }

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
           /// Nullable:False
           /// </summary>
           public string Receiver_Name { get; set; }

           /// <summary>
           /// Desc:账单号
           /// Default:
           /// Nullable:False
           /// </summary>
           public string BillNo { get; set; }

           /// <summary>
           /// Desc:收货人电话
           /// Default:
           /// Nullable:False
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
           /// Nullable:False
           /// </summary>
           public string Receiver_Address { get; set; }

           /// <summary>
           /// Desc:订单状态(1:代付款,2:待发货,3:待收货.4已完成)
           /// Default:1
           /// Nullable:False
           /// </summary>
           public byte OrderStatus { get; set; }

           /// <summary>
           /// Desc:客戶留言
           /// Default:
           /// Nullable:True
           /// </summary>
           public string CustomerMessage { get; set; }

           /// <summary>
           /// Desc:(1 在线支付,2货到付款)
           /// Default:1
           /// Nullable:False
           /// </summary>
           public byte Distribution { get; set; }

           /// <summary>
           /// Desc:支付状态(1:未支付,2:已支付)
           /// Default:1
           /// Nullable:False
           /// </summary>
           public byte PayStatus { get; set; }


    }
}
