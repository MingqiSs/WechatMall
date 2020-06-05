using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace X.Models.WMDB
{
    [Serializable]
    public partial class wm_product
    {
           public wm_product(){}

           /// <summary>
           /// Desc:
           /// Default:b'0'
           /// Nullable:False
           /// </summary>
           public bool Hot { get; set; }

           /// <summary>
           /// Desc:商品分类id
           /// Default:0
           /// Nullable:False
           /// </summary>
           public int ProductTypeID { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public string Icon { get; set; }

           /// <summary>
           /// Desc:备注
           /// Default:
           /// Nullable:True
           /// </summary>
           public string Remark { get; set; }

           /// <summary>
           /// Desc:商品ID
           /// Default:
           /// Nullable:False
           /// </summary>
           [Key]
           public string ID { get; set; }

           /// <summary>
           /// Desc:销量
           /// Default:0
           /// Nullable:False
           /// </summary>
           public int Sales { get; set; }

           /// <summary>
           /// Desc:图片
           /// Default:
           /// Nullable:True
           /// </summary>
           public string Image { get; set; }

           /// <summary>
           /// Desc:
           /// Default:0
           /// Nullable:False
           /// </summary>
           public int Sort { get; set; }

           /// <summary>
           /// Desc:库存
           /// Default:0
           /// Nullable:False
           /// </summary>
           public int Inventory { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public DateTime CrateTime { get; set; }

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
           public byte DataStatus { get; set; }

           /// <summary>
           /// Desc:商品名称
           /// Default:
           /// Nullable:False
           /// </summary>
           public string Name { get; set; }

           /// <summary>
           /// Desc:商品描述
           /// Default:
           /// Nullable:False
           /// </summary>
           public string Describe { get; set; }

           /// <summary>
           /// Desc:价格
           /// Default:0.00
           /// Nullable:False
           /// </summary>
           public decimal Price { get; set; }


    }
}
