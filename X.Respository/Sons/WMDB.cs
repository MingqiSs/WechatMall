using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;

using SqlSugar;
namespace X.Respository.Sons.WMDB
{
    public partial class Sys_City:BaseRespository<X.Models.WMDB.Sys_City>,X.IRespository.Sons.WMDB.ISys_City
    {
           public override SqlSugarClient db
           {
               get
               {
                   return DBOperation.GetClient_WMDB();
               }
           }
    }
    public partial class Sys_Log:BaseRespository<X.Models.WMDB.Sys_Log>,X.IRespository.Sons.WMDB.ISys_Log
    {
           public override SqlSugarClient db
           {
               get
               {
                   return DBOperation.GetClient_WMDB();
               }
           }
    }
    public partial class Sys_Menu:BaseRespository<X.Models.WMDB.Sys_Menu>,X.IRespository.Sons.WMDB.ISys_Menu
    {
           public override SqlSugarClient db
           {
               get
               {
                   return DBOperation.GetClient_WMDB();
               }
           }
    }
    public partial class Sys_Province:BaseRespository<X.Models.WMDB.Sys_Province>,X.IRespository.Sons.WMDB.ISys_Province
    {
           public override SqlSugarClient db
           {
               get
               {
                   return DBOperation.GetClient_WMDB();
               }
           }
    }
    public partial class Sys_Role:BaseRespository<X.Models.WMDB.Sys_Role>,X.IRespository.Sons.WMDB.ISys_Role
    {
           public override SqlSugarClient db
           {
               get
               {
                   return DBOperation.GetClient_WMDB();
               }
           }
    }
    public partial class Sys_RoleAuth:BaseRespository<X.Models.WMDB.Sys_RoleAuth>,X.IRespository.Sons.WMDB.ISys_RoleAuth
    {
           public override SqlSugarClient db
           {
               get
               {
                   return DBOperation.GetClient_WMDB();
               }
           }
    }
    public partial class Sys_Setting:BaseRespository<X.Models.WMDB.Sys_Setting>,X.IRespository.Sons.WMDB.ISys_Setting
    {
           public override SqlSugarClient db
           {
               get
               {
                   return DBOperation.GetClient_WMDB();
               }
           }
    }
    public partial class Sys_User:BaseRespository<X.Models.WMDB.Sys_User>,X.IRespository.Sons.WMDB.ISys_User
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
