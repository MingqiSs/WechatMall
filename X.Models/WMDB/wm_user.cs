using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace X.Models.WMDB
{
    [Serializable]
    public partial class wm_user
    {
           public wm_user(){}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public string Name { get; set; }

           /// <summary>
           /// Desc:
           /// Default:CURRENT_TIMESTAMP
           /// Nullable:False
           /// </summary>
           public DateTime CreateTime { get; set; }

           /// <summary>
           /// Desc:微信appID
           /// Default:
           /// Nullable:True
           /// </summary>
           public string WeChatAppID { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public DateTime? ModifyTime { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public DateTime? BirthDate { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public byte DataStatus { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public string HeadImg { get; set; }

           /// <summary>
           /// Desc:手机号
           /// Default:
           /// Nullable:False
           /// </summary>
           public string Mobile { get; set; }

           /// <summary>
           /// Desc:密码
           /// Default:
           /// Nullable:False
           /// </summary>
           public string Pwd { get; set; }

           /// <summary>
           /// Desc:邮箱
           /// Default:
           /// Nullable:True
           /// </summary>
           public string Email { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           [Key]
           public string UID { get; set; }

           /// <summary>
           /// Desc:昵称
           /// Default:
           /// Nullable:True
           /// </summary>
           public string Nickname { get; set; }


    }
}
