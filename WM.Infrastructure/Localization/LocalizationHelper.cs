using System;
using System.Globalization;
using WM.Infrastructure.Localization.Configuration;
using WM.Infrastructure.Localization.Dictionaries;
using WM.Infrastructure.Localization.Dictionaries.Xml;
using WM.Infrastructure.Localization.Sources;
using WM.Infrastructure.Localization;
using WM.Infrastructure.Models;

namespace WM.Infrastructure.Localization
{
    /// <summary>
    /// 此静态类用于简化获取本地化字符串。
    /// </summary>
    public static class LocalizationHelper
    {
        private static string _currentCultureName;
        private static string _currentSourceName;

        /// <summary>
        /// 获取或设置本地化区域信息的名称。
        /// </summary>
        public static string CurrentCultureName
        {
            get => _currentCultureName.IsNullOrWhiteSpace() ? AppConsts.DefaultCultureName : _currentCultureName;
            set => _currentCultureName = value;
        }

        /// <summary>
        /// 获取或设置本地化资源的名称。
        /// </summary>
        public static string CurrentSourceName
        {
            get => _currentSourceName.IsNullOrWhiteSpace() ? AppConsts.LocalizationSourceName : _currentSourceName;
            set => _currentSourceName = value;
        }

        /// <summary>
        ///获取对本地化管理器的引用。
        /// </summary>
        public static ILocalizationManager Manager => LocalizationManager.Value;
        public static ILocalizationConfiguration Configuration => LocalizationConfiguration.Value;
        public static ILocalizationContext Context => LocalizationContext.Value;

        private static readonly Lazy<ILocalizationConfiguration> LocalizationConfiguration;
        private static readonly Lazy<ILocalizationManager> LocalizationManager;
        private static readonly Lazy<ILocalizationContext> LocalizationContext;
        static LocalizationHelper()
        {
            LocalizationConfiguration = new Lazy<ILocalizationConfiguration>(CreateLocalizationConfiguration);
            LocalizationManager = new Lazy<ILocalizationManager>(CreateLocalizationManager);
            LocalizationContext = new Lazy<ILocalizationContext>(() => CreateLocalizationContext(Manager));
        }

        private static ILocalizationManager CreateLocalizationManager()
        {
            var localizationManager = new LocalizationManager(Configuration);
            localizationManager.Initialize();
            return localizationManager;
        }

        private static ILocalizationContext CreateLocalizationContext(ILocalizationManager localizationManager)
        {
            var localizationContext = new LocalizationContext(localizationManager);
            return localizationContext;
        }

        private static ILocalizationConfiguration CreateLocalizationConfiguration()
        {
            var localizationConfiguration = new LocalizationConfiguration();
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(CurrentSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(LocalizationConfigurer).Assembly,
                        "WM.Infrastructure.Localization.Sources.Files"
                    )
                )
            );
            //localizationConfiguration.Sources.Add(
            //    new DictionaryBasedLocalizationSource("Xsmcfx-en",
            //        //D:\Projects\MCFX.NET-2.0\WM.Infrastructure\Localization\Sources\Files
            //        new XmlFileLocalizationDictionaryProvider("D://Projects/MCFX.NET-2.0/WM.Infrastructure/Localization/Sources/Files")
            //    )
            //);
            return localizationConfiguration;
        }

        /// <summary>
        /// 获取预先注册的本地化源。
        /// </summary>
        /// <param name="name">本地化源的名称。</param>
        /// <returns>返回本地化源。</returns>
        public static ILocalizationSource GetSource(string name)
        {
            return LocalizationManager.Value.GetSource(name);
        }

        /// <summary>
        /// 获取指定语言的本地化字符串。
        /// </summary>
        /// <param name="name">获取本地化字符串的键名。</param>
        /// <returns>返回本地化字符串。</returns>
        public static string GetString(string name)
        {
            return LocalizationManager.Value.GetString(CurrentSourceName, name, new CultureInfo(CurrentCultureName));
        }

        /// <summary>
        /// 获取指定语言的本地化字符串。
        /// </summary>
        /// <param name="sourceName">本地化源的名称。</param>
        /// <param name="name">获取本地化字符串的键名。</param>
        /// <param name="culture">本地化区域信息。</param>
        /// <returns>返回本地化字符串。</returns>
        public static string GetString(string name, CultureInfo culture, string sourceName)
        {
            return LocalizationManager.Value.GetString(sourceName, name, culture);
        }

        /// <summary>
        /// 获取指定语言的本地化字符串。
        /// </summary>
        /// <param name="name">获取本地化字符串的键名。</param>
        /// <param name="culture">本地化区域信息。</param>
        /// <returns>返回本地化字符串。</returns>
        public static string GetString(string name, CultureInfo culture)
        {
            return LocalizationManager.Value.GetString(CurrentSourceName, name, culture);
        }

        /// <summary>
        /// 获取指定语言的本地化字符串。
        /// </summary>
        /// <param name="sourceName">本地化源的名称。</param>
        /// <param name="name">获取本地化字符串的键名。</param>
        /// <param name="args">格式化参数。</param>
        /// <returns>返回本地化字符串。</returns>
        public static string GetString(string sourceName, string name, params object[] args)
        {
            return LocalizationManager.Value.GetString(sourceName, name, args);
        }

        /// <summary>
        /// 获取指定语言的本地化字符串。
        /// </summary>
        /// <param name="name">获取本地化字符串的键名。</param>
        /// <param name="args">格式化参数。</param>
        /// <returns>返回本地化字符串。</returns>
        public static string GetString(string name, params object[] args)
        {
            return LocalizationManager.Value.GetString(CurrentSourceName, name, args);
        }

        /// <summary>
        ///获取指定语言的本地化字符串。
        /// </summary>
        /// <param name="sourceName">本地化源的名称。</param>
        /// <param name="name">获取本地化字符串的键名。</param>
        /// <param name="culture">本地化区域信息。</param>
        /// <param name="args">格式化参数。</param>
        /// <returns>返回本地化字符串。</returns>
        public static string GetString(string sourceName, string name, CultureInfo culture, params object[] args)
        {
            return LocalizationManager.Value.GetString(sourceName, name, culture, args);
        }

        /// <summary>
        /// 获取指定语言的本地化字符串。
        /// </summary>
        /// <param name="name">获取本地化字符串的键名。</param>
        /// <param name="culture">本地化区域信息。</param>
        /// <param name="args">格式化参数。</param>
        /// <returns>返回本地化字符串。</returns>
        public static string GetString(string name, CultureInfo culture, params object[] args)
        {
            return LocalizationManager.Value.GetString(CurrentSourceName, name, culture, args);
        }

        /// <summary>
        /// 获取 <paramref name="name"/> 对应的本地化字符串。
        /// </summary>
        /// <param name="name">本地化资源的名称。</param>
        /// <param name="args">格式化参数。</param>
        /// <returns>返回本地化字符串。</returns>
        public static string L(string name, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));

            var value = GetString(name, args);
            return value;
        }

        /// <summary>
        /// 获取 <paramref name="name"/> 对应的本地化字符串。
        /// </summary>
        /// <param name="name">本地化资源的名称。</param>
        /// <returns>返回本地化字符串。</returns>
        public static string L(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));

            var value = GetString(name);
            return value;
        }
        /// <summary>
        /// 初始化语言
        /// </summary>
        /// <param name="cultureName"></param>
        public static void InitializeLanguage(string cultureName)
        {
            if (!string.IsNullOrEmpty(cultureName))
            {
                if (cultureName.ToLower()== AppConsts.DefaultCultureName.ToLower())
                    _currentCultureName = AppConsts.DefaultCultureName;
                else if (cultureName.ToLower()== AppConsts.DefaultCultureNameCN.ToLower())
                    _currentCultureName = AppConsts.DefaultCultureNameCN;
            }
          
        }
    }
}