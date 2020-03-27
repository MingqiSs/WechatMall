using System;
using System.Linq;
using System.Text;

namespace X.IRespository.DBSession
{
    public partial interface IWMDBSession:X.IRespository.IRespositorySession
    {
           X.IRespository.Sons.WMDB.Icm_city cm_city {get;}
           X.IRespository.Sons.WMDB.Icm_province cm_province {get;}
           X.IRespository.Sons.WMDB.Icm_setting cm_setting {get;}
           X.IRespository.Sons.WMDB.Ir_product_tag r_product_tag {get;}
           X.IRespository.Sons.WMDB.Iwm_order wm_order {get;}
           X.IRespository.Sons.WMDB.Iwm_order_card wm_order_card {get;}
           X.IRespository.Sons.WMDB.Iwm_order_card_info wm_order_card_info {get;}
           X.IRespository.Sons.WMDB.Iwm_order_info wm_order_info {get;}
           X.IRespository.Sons.WMDB.Iwm_order_logistics wm_order_logistics {get;}
           X.IRespository.Sons.WMDB.Iwm_order_logistics_flow wm_order_logistics_flow {get;}
           X.IRespository.Sons.WMDB.Iwm_order_pay_history wm_order_pay_history {get;}
           X.IRespository.Sons.WMDB.Iwm_product wm_product {get;}
           X.IRespository.Sons.WMDB.Iwm_product_tag wm_product_tag {get;}
           X.IRespository.Sons.WMDB.Iwm_product_type wm_product_type {get;}
           X.IRespository.Sons.WMDB.Iwm_user wm_user {get;}
           X.IRespository.Sons.WMDB.Iwm_user_shopping_address wm_user_shopping_address {get;}

    }
}
