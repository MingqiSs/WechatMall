using System;
using System.Collections.Generic;
using System.Text;
using X.Models.WMDB;

namespace WM.Service.Domain.Interface
{
    public interface IUserDomainService
    {
        wm_user GetUserByUID(string uid);

        wm_user GetUserByMobileOrEmail(string name);
    }
}
