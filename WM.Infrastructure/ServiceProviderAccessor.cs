using System;
using System.Collections.Generic;
using System.Text;

namespace WM.Infrastructure
{
    public class ServiceProviderAccessor
    {
        private static IServiceProvider serviceProvider;

        public static void SetServiceProvider(IServiceProvider sp)
        {
            serviceProvider = sp;
        }
        public static IServiceProvider ServiceProvider => serviceProvider;
    }
}
