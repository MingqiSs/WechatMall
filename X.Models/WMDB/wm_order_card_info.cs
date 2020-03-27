using System;
using System.Linq;
using System.Text;

namespace X.Models.WMDB
{
    [Serializable]
    public partial class wm_order_card_info
    {
           public wm_order_card_info(){}

           /// <summary>
           /// Desc:数据状态
           /// Default:
           /// Nullable:False
           /// </summary>
           public byte DataStatus { get; set; }

           /// <summary>
           /// Desc:创建时间
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
           /// Desc:购物车ID
           /// Default:
           /// Nullable:False
           /// </summary>
           public string Order_CardID { get; set; }

           /// <summary>
           /// Desc:商品id
           /// Default:1
           /// Nullable:False
           /// </summary>
           public int ProductID { get; set; }

           /// <summary>
           /// Desc:商品数
           /// Default:0
           /// Nullable:False
           /// </summary>
           public int Product_Num { get; set; }


    }
}
