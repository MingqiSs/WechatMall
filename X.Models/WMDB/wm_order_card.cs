using System;
using System.Linq;
using System.Text;

namespace X.Models.WMDB
{
    [Serializable]
    public partial class wm_order_card
    {
           public wm_order_card(){}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public DateTime CreateTime { get; set; }

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
           public int ID { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public string UID { get; set; }

           /// <summary>
           /// Desc:
           /// Default:b'0'
           /// Nullable:False
           /// </summary>
           public bool Checked { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public byte DataStauts { get; set; }


    }
}
