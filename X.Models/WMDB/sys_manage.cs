using System;
using System.Linq;
using System.Text;

namespace X.Models.WMDB
{
    [Serializable]
    public partial class sys_manage
    {
           public sys_manage(){}

           /// <summary>
           /// Desc:创建时间
           /// Default:CURRENT_TIMESTAMP
           /// Nullable:False
           /// </summary>
           public DateTime CreateTime { get; set; }

           /// <summary>
           /// Desc:是否启用
           /// Default:1
           /// Nullable:False
           /// </summary>
           public byte DataStatus { get; set; }

           /// <summary>
           /// Desc:
           /// Default:newid()
           /// Nullable:False
           /// </summary>
           public string ID { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public int RoleId { get; set; }

           /// <summary>
           /// Desc:用户名
           /// Default:
           /// Nullable:False
           /// </summary>
           public string UserName { get; set; }

           /// <summary>
           /// Desc:密码
           /// Default:
           /// Nullable:False
           /// </summary>
           public string Pwd { get; set; }

           /// <summary>
           /// Desc:真实姓名
           /// Default:
           /// Nullable:False
           /// </summary>
           public string TrueName { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public string Email { get; set; }

           /// <summary>
           /// Desc:IP地址
           /// Default:
           /// Nullable:False
           /// </summary>
           public string IP { get; set; }


    }
}
