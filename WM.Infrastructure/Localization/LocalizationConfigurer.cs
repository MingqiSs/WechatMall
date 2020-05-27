using System.Globalization;
using System.Reflection;
using MC.Common.Localization.Configuration;
using MC.Common.Localization.Dictionaries;
using MC.Common.Localization.Dictionaries.Xml;

namespace MC.Common.Localization
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
            //    new DictionaryBasedLocalizationSource("MC.Common.Localization.Sources.Files",
            //        new XmlEmbeddedFileLocalizationDictionaryProvider(
            //           Assembly.GetExecutingAssembly(),
            //            "MC.Common.Localization.Sources.Files"
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
