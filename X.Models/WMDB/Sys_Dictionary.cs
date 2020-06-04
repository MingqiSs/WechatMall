using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace X.Models.WMDB
{
    [Serializable]
    public partial class Sys_Dictionary
    {
           public Sys_Dictionary(){}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public string DBServer { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public int? ModifyID { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public string DbSql { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public int? OrderNo { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           [Key]
           public int Dic_ID { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public int ParentId { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public string DicName { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public string Remark { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public string Config { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public string DicNo { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public DateTime? CreateDate { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public byte Enable { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public int? CreateID { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public string Modifier { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public string Creator { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public DateTime? ModifyDate { get; set; }


    }
}
