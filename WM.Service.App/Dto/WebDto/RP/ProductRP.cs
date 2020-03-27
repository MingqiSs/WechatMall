using System;
using System.Collections.Generic;
using System.Text;

namespace WM.Service.App.Dto.WebDto.RP
{
   
    /// <summary>
    /// 
    /// </summary>
    public class ProductRP
    {
        /// <summary>
        /// Desc:商品ID
        /// Default:
        /// Nullable:False
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Desc:商品名称
        /// Default:
        /// Nullable:False
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Desc:价格
        /// Default:0.00
        /// Nullable:False
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Desc:备注
        /// Default:
        /// Nullable:True
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Desc:图片
        /// Default:
        /// Nullable:True
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Desc:销量
        /// Default:0
        /// Nullable:False
        /// </summary>
        public int Sales { get; set; }

        /// <summary>
        /// Desc:库存
        /// Default:0
        /// Nullable:False
        /// </summary>
        public int Inventory { get; set; }


        /// <summary>
        /// Desc:是否推荐
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
        /// Desc:排序号
        /// Default:0
        /// Nullable:False
        /// </summary>
        public int Sort { get; set; }

   
        /// <summary>
        /// Desc:商品描述
        /// Default:
        /// Nullable:False
        /// </summary>
        public string Describe { get; set; }
    }

    public class ProductTypeRP
    {
        /// <summary>
        /// Desc:商品类型ID
        /// Default:
        /// Nullable:False
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Desc:商品类型名称
        /// Default:
        /// Nullable:False
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Desc:商品排序号
        /// Default:0
        /// Nullable:False
        /// </summary>
        public int Sort { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ProductTagRP : ProductTypeRP
    {
    }
}
