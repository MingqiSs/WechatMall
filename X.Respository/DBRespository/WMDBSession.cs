using System;
using System.Linq;
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
           public X.IRespository.Sons.WMDB.Iwm_user wm_user
           {
               get { return new X.Respository.Sons.WMDB.wm_user(); }
           }

    }
    }
