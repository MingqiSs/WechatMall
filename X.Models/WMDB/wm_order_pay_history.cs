using System;
using System.Linq;
using System.Text;

namespace X.Models.WMDB
{
    [Serializable]
    public partial class wm_order_pay_history
    {
           public wm_order_pay_history(){}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public decimal Order_Amount { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public string Payment_Amount { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public string Pay_Json { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public string Remark { get; set; }

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
           public int BillNo { get; set; }

           /// <summary>
           /// Desc:支付方式(1:微信)
           /// Default:1
           /// Nullable:False
           /// </summary>
           public int PaymentType { get; set; }


    }
}
