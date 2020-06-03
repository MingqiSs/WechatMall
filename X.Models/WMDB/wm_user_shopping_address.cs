using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace X.Models.WMDB
{
    [Serializable]
    public partial class wm_user_shopping_address
    {
           public wm_user_shopping_address(){}

           /// <summary>
           /// Desc:市
           /// Default:
           /// Nullable:False
           /// </summary>
           public int CityID { get; set; }

           /// <summary>
           /// Desc:联系人详细地址
           /// Default:
           /// Nullable:False
           /// </summary>
           public string Receiver_Address { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public DateTime CreateTime { get; set; }

           /// <summary>
           /// Desc:联系人分名称
           /// Default:
           /// Nullable:False
           /// </summary>
           public string Receiver_Name { get; set; }

           /// <summary>
           /// Desc:
           /// Default:1
           /// Nullable:False
           /// </summary>
           public byte DataStatus { get; set; }

           /// <summary>
           /// Desc:联系人电话
           /// Default:
           /// Nullable:False
           /// </summary>
           public string Receiver_Phone { get; set; }

           /// <summary>
           /// Desc:区
           /// Default:
           /// Nullable:False
           /// </summary>
           public int DistrictID { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public string UID { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           [Key]
           public int ID { get; set; }

           /// <summary>
           /// Desc:默认
           /// Default:b'0'
           /// Nullable:False
           /// </summary>
           public bool Isdef { get; set; }

           /// <summary>
           /// Desc:修改时间
           /// Default:
           /// Nullable:True
           /// </summary>
           public DateTime? ModifyTime { get; set; }

           /// <summary>
           /// Desc:省
           /// Default:
           /// Nullable:False
           /// </summary>
           public int ProvinceID { get; set; }


    }
}
