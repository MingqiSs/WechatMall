using System.Globalization;
using System.Reflection;
using WM.Infrastructure.Localization.Configuration;
using WM.Infrastructure.Localization.Dictionaries;
using WM.Infrastructure.Localization.Dictionaries.Xml;

namespace WM.Infrastructure.Localization
{
    public static class LocalizationConfigurer
    {
        public static ILocalizationConfiguration Create()
        {
            var config=new LocalizationConfiguration();
            Configure(config);
            return config;
        }

        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            //localizationConfiguration.Sources.Add(
            //    new DictionaryBasedLocalizationSource("WM.Infrastructure.Localization.Sources.Files",
            //        new XmlEmbeddedFileLocalizationDictionaryProvider(
            //           Assembly.GetExecutingAssembly(),
            //            "WM.Infrastructure.Localization.Sources.Files"
            //        )
            //    )
            //);
            //localizationConfiguration.Sources.Add(
            //    new DictionaryBasedLocalizationSource(AppConsts.LocalizationSourceName,
            //        new XmlFileLocalizationDictionaryProvider("c://")
            //    )
            //);
        }
    }
}
