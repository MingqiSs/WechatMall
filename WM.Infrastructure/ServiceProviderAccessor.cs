using System;
using System.Collections.Generic;
using System.Text;

namespace WM.Infrastructure
{
    public class ServiceProviderAccessor
    {
        public static void SetServiceProvider(IServiceProvider sp)
        {
            ServiceProvider = sp;
        }
        public static IServiceProvider ServiceProvider { get; private set; }
    }
}
