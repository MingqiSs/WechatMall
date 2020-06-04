using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;

using SqlSugar;
namespace X.Respository.DBRespository
{
public partial class WMDBSession : X.Respository.RespositorySession, X.IRespository.DBSession.IWMDBSession
    {
           public override SqlSugarClient DB
           {
               get
               {
                   return DBOperation.GetClient_WMDB();
               }
           }
           public X.IRespository.Sons.WMDB.ISys_City Sys_City
           {
               get { return new X.Respository.Sons.WMDB.Sys_City(); }
           }
           public X.IRespository.Sons.WMDB.ISys_Dictionary Sys_Dictionary
           {
               get { return new X.Respository.Sons.WMDB.Sys_Dictionary(); }
           }
           public X.IRespository.Sons.WMDB.ISys_DictionaryList Sys_DictionaryList
           {
               get { return new X.Respository.Sons.WMDB.Sys_DictionaryList(); }
           }
           public X.IRespository.Sons.WMDB.ISys_Log Sys_Log
           {
               get { return new X.Respository.Sons.WMDB.Sys_Log(); }
           }
           public X.IRespository.Sons.WMDB.ISys_Menu Sys_Menu
           {
               get { return new X.Respository.Sons.WMDB.Sys_Menu(); }
           }
           public X.IRespository.Sons.WMDB.ISys_Province Sys_Province
           {
               get { return new X.Respository.Sons.WMDB.Sys_Province(); }
           }
           public X.IRespository.Sons.WMDB.ISys_Role Sys_Role
           {
               get { return new X.Respository.Sons.WMDB.Sys_Role(); }
           }
           public X.IRespository.Sons.WMDB.ISys_RoleAuth Sys_RoleAuth
           {
               get { return new X.Respository.Sons.WMDB.Sys_RoleAuth(); }
           }
           public X.IRespository.Sons.WMDB.ISys_Setting Sys_Setting
           {
               get { return new X.Respository.Sons.WMDB.Sys_Setting(); }
           }
           public X.IRespository.Sons.WMDB.ISys_User Sys_User
           {
               get { return new X.Respository.Sons.WMDB.Sys_User(); }
           }
           public X.IRespository.Sons.WMDB.Ir_product_tag r_product_tag
           {
               get { return new X.Respository.Sons.WMDB.r_product_tag(); }
           }
           public X.IRespository.Sons.WMDB.Iwm_order wm_order
           {
               get { return new X.Respository.Sons.WMDB.wm_order(); }
           }
           public X.IRespository.Sons.WMDB.Iwm_order_card wm_order_card
           {
               get { return new X.Respository.Sons.WMDB.wm_order_card(); }
           }
           public X.IRespository.Sons.WMDB.Iwm_order_card_info wm_order_card_info
           {
               get { return new X.Respository.Sons.WMDB.wm_order_card_info(); }
           }
           public X.IRespository.Sons.WMDB.Iwm_order_info wm_order_info
           {
               get { return new X.Respository.Sons.WMDB.wm_order_info(); }
           }
           public X.IRespository.Sons.WMDB.Iwm_order_logistics wm_order_logistics
           {
               get { return new X.Respository.Sons.WMDB.wm_order_logistics(); }
           }
           public X.IRespository.Sons.WMDB.Iwm_order_logistics_flow wm_order_logistics_flow
           {
               get { return new X.Respository.Sons.WMDB.wm_order_logistics_flow(); }
           }
           public X.IRespository.Sons.WMDB.Iwm_order_pay_history wm_order_pay_history
           {
               get { return new X.Respository.Sons.WMDB.wm_order_pay_history(); }
           }
           public X.IRespository.Sons.WMDB.Iwm_product wm_product
           {
               get { return new X.Respository.Sons.WMDB.wm_product(); }
           }
           public X.IRespository.Sons.WMDB.Iwm_product_tag wm_product_tag
           {
               get { return new X.Respository.Sons.WMDB.wm_product_tag(); }
           }
           public X.IRespository.Sons.WMDB.Iwm_product_type wm_product_type
           {
               get { return new X.Respository.Sons.WMDB.wm_product_type(); }
           }
           public X.IRespository.Sons.WMDB.Iwm_user wm_user
           {
               get { return new X.Respository.Sons.WMDB.wm_user(); }
           }
           public X.IRespository.Sons.WMDB.Iwm_user_shopping_address wm_user_shopping_address
           {
               get { return new X.Respository.Sons.WMDB.wm_user_shopping_address(); }
           }

    }
    }
