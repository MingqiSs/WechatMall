using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace X.Models.WMDB
{
    [Serializable]
    public partial class wm_order_info
    {
           public wm_order_info(){}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public int OrderId { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public string Product_Icon { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public string Product_Name { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public decimal Product_Price { get; set; }

           /// <summary>
           /// Desc:数量
           /// Default:0
           /// Nullable:False
           /// </summary>
           public int Product_Num { get; set; }

           /// <summary>
           /// Desc:
           /// Default:1
           /// Nullable:False
           /// </summary>
           public byte DataStauts { get; set; }

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
           [Key]
           public int ID { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public string ProductID { get; set; }


    }
}
