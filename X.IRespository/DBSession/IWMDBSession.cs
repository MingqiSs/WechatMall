using System;
using System.Linq;
using System.Text;

namespace X.IRespository.DBSession
{
    public partial interface IWMDBSession:X.IRespository.IRespositorySession
    {
           X.IRespository.Sons.WMDB.Iwm_user wm_user {get;}

    }
}
