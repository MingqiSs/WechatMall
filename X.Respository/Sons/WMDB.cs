using System;
using System.Linq;
using System.Text;

using SqlSugar;
namespace X.Respository.Sons.WMDB
{
    public partial class cm_city:BaseRespository<X.Models.WMDB.cm_city>,X.IRespository.Sons.WMDB.Icm_city
    {
           public override SqlSugarClient db
           {
               get
               {
                   return DBOperation.GetClient_WMDB();
               }
           }
    }
    public partial class cm_province:BaseRespository<X.Models.WMDB.cm_province>,X.IRespository.Sons.WMDB.Icm_province
    {
           public override SqlSugarClient db
           {
               get
               {
                   return DBOperation.GetClient_WMDB();
               }
           }
    }
    public partial class cm_setting:BaseRespository<X.Models.WMDB.cm_setting>,X.IRespository.Sons.WMDB.Icm_setting
    {
           public override SqlSugarClient db
           {
               get
               {
                   return DBOperation.GetClient_WMDB();
               }
           }
    }
    public partial class r_product_tag:BaseRespository<X.Models.WMDB.r_product_tag>,X.IRespository.Sons.WMDB.Ir_product_tag
    {
           public override SqlSugarClient db
           {
               get
               {
                   return DBOperation.GetClient_WMDB();
               }
           }
    }
    public partial class sys_manage:BaseRespository<X.Models.WMDB.sys_manage>,X.IRespository.Sons.WMDB.Isys_manage
    {
           public override SqlSugarClient db
           {
               get
               {
                   return DBOperation.GetClient_WMDB();
               }
           }
    }
    public partial class wm_order:BaseRespository<X.Models.WMDB.wm_order>,X.IRespository.Sons.WMDB.Iwm_order
    {
           public override SqlSugarClient db
           {
               get
               {
                   return DBOperation.GetClient_WMDB();
               }
           }
    }
    public partial class wm_order_card:BaseRespository<X.Models.WMDB.wm_order_card>,X.IRespository.Sons.WMDB.Iwm_order_card
    {
           public override SqlSugarClient db
           {
               get
               {
                   return DBOperation.GetClient_WMDB();
               }
           }
    }
    public partial class wm_order_card_info:BaseRespository<X.Models.WMDB.wm_order_card_info>,X.IRespository.Sons.WMDB.Iwm_order_card_info
    {
           public override SqlSugarClient db
           {
               get
               {
                   return DBOperation.GetClient_WMDB();
               }
           }
    }
    public partial class wm_order_info:BaseRespository<X.Models.WMDB.wm_order_info>,X.IRespository.Sons.WMDB.Iwm_order_info
    {
           public override SqlSugarClient db
           {
               get
               {
                   return DBOperation.GetClient_WMDB();
               }
           }
    }
    public partial class wm_order_logistics:BaseRespository<X.Models.WMDB.wm_order_logistics>,X.IRespository.Sons.WMDB.Iwm_order_logistics
    {
           public override SqlSugarClient db
           {
               get
               {
                   return DBOperation.GetClient_WMDB();
               }
           }
    }
    public partial class wm_order_logistics_flow:BaseRespository<X.Models.WMDB.wm_order_logistics_flow>,X.IRespository.Sons.WMDB.Iwm_order_logistics_flow
    {
           public override SqlSugarClient db
           {
               get
               {
                   return DBOperation.GetClient_WMDB();
               }
           }
    }
    public partial class wm_order_pay_history:BaseRespository<X.Models.WMDB.wm_order_pay_history>,X.IRespository.Sons.WMDB.Iwm_order_pay_history
    {
           public override SqlSugarClient db
           {
               get
               {
                   return DBOperation.GetClient_WMDB();
               }
           }
    }
    public partial class wm_product:BaseRespository<X.Models.WMDB.wm_product>,X.IRespository.Sons.WMDB.Iwm_product
    {
           public override SqlSugarClient db
           {
               get
               {
                   return DBOperation.GetClient_WMDB();
               }
           }
    }
    public partial class wm_product_tag:BaseRespository<X.Models.WMDB.wm_product_tag>,X.IRespository.Sons.WMDB.Iwm_product_tag
    {
           public override SqlSugarClient db
           {
               get
               {
                   return DBOperation.GetClient_WMDB();
               }
           }
    }
    public partial class wm_product_type:BaseRespository<X.Models.WMDB.wm_product_type>,X.IRespository.Sons.WMDB.Iwm_product_type
    {
           public override SqlSugarClient db
           {
               get
               {
                   return DBOperation.GetClient_WMDB();
               }
           }
    }
    public partial class wm_user:BaseRespository<X.Models.WMDB.wm_user>,X.IRespository.Sons.WMDB.Iwm_user
    {
           public override SqlSugarClient db
           {
               get
               {
                   return DBOperation.GetClient_WMDB();
               }
           }
    }
    public partial class wm_user_shopping_address:BaseRespository<X.Models.WMDB.wm_user_shopping_address>,X.IRespository.Sons.WMDB.Iwm_user_shopping_address
    {
           public override SqlSugarClient db
           {
               get
               {
                   return DBOperation.GetClient_WMDB();
               }
           }
    }

}
