using System;
using System.Linq;
using System.Text;

using SqlSugar;
namespace X.Respository.Sons.WMDB
{
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

}
