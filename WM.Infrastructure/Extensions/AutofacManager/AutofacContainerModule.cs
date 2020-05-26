using System;
using System.Collections.Generic;
using System.Text;

namespace WM.Infrastructure.Extensions.AutofacManager
{
    public static class AutofacContainerModule
    {
        public static TService GetService<TService>() where TService : class
        {
            if(ServiceProviderAccessor.ServiceProvider==null) throw new DllNotFoundException($"the  \"{nameof(AutofacContainerModule)}\" Is Null");

            return ServiceProviderAccessor.ServiceProvider.GetService(typeof(TService)) as TService;
           // return typeof(TService).GetService() as TService;
        }


        //public static object GetService(this Type serviceType)
        //{
        //    // HttpContext.Current.RequestServices.GetRequiredService<T>(serviceType);
        //    return Utilities.HttpContext.Current.RequestServices.GetService(serviceType);
        //}
    }
    
}
