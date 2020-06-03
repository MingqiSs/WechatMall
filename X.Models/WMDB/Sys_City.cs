using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace X.Models.WMDB
{
    [Serializable]
    public partial class Sys_City
    {
           public Sys_City(){}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public string Code { get; set; }

           /// <summary>
           /// Desc:
           /// Default:CURRENT_TIMESTAMP
           /// Nullable:False
           /// </summary>
           public DateTime CreateTime { get; set; }

           /// <summary>
           /// Desc:
           /// Default:A
           /// Nullable:False
           /// </summary>
           public string FirstChar { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           [Key]
           public int ID { get; set; }

           /// <summary>
           /// Desc:
           /// Default:b'0'
           /// Nullable:False
           /// </summary>
           public bool IsHot { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public string Name { get; set; }

           /// <summary>
           /// Desc:
           /// Default:0
           /// Nullable:True
           /// </summary>
           public int? ProvinceID { get; set; }

           /// <summary>
           /// Desc:
           /// Default:100
           /// Nullable:False
           /// </summary>
           public byte Sort { get; set; }


    }
}
