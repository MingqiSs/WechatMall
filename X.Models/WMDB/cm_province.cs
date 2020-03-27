using System;
using System.Linq;
using System.Text;

namespace X.Models.WMDB
{
    [Serializable]
    public partial class cm_province
    {
           public cm_province(){}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public string Name { get; set; }

           /// <summary>
           /// Desc:
           /// Default:CURRENT_TIMESTAMP
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
           /// Nullable:False
           /// </summary>
           public string Code { get; set; }


    }
}
