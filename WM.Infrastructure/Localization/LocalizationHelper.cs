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
    /// �˾�̬�����ڼ򻯻�ȡ���ػ��ַ�����
    /// </summary>
    public static class LocalizationHelper
    {
        private static string _currentCultureName;
        private static string _currentSourceName;

        /// <summary>
        /// ��ȡ�����ñ��ػ�������Ϣ�����ơ�
        /// </summary>
        public static string CurrentCultureName
        {
            get => _currentCultureName.IsNullOrWhiteSpace() ? AppConsts.DefaultCultureName : _currentCultureName;
            set => _currentCultureName = value;
        }

        /// <summary>
        /// ��ȡ�����ñ��ػ���Դ�����ơ�
        /// </summary>
        public static string CurrentSourceName
        {
            get => _currentSourceName.IsNullOrWhiteSpace() ? AppConsts.LocalizationSourceName : _currentSourceName;
            set => _currentSourceName = value;
        }

        /// <summary>
        ///��ȡ�Ա��ػ������������á�
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
        /// ��ȡԤ��ע��ı��ػ�Դ��
        /// </summary>
        /// <param name="name">���ػ�Դ�����ơ�</param>
        /// <returns>���ر��ػ�Դ��</returns>
        public static ILocalizationSource GetSource(string name)
        {
            return LocalizationManager.Value.GetSource(name);
        }

        /// <summary>
        /// ��ȡָ�����Եı��ػ��ַ�����
        /// </summary>
        /// <param name="name">��ȡ���ػ��ַ����ļ�����</param>
        /// <returns>���ر��ػ��ַ�����</returns>
        public static string GetString(string name)
        {
            return LocalizationManager.Value.GetString(CurrentSourceName, name, new CultureInfo(CurrentCultureName));
        }

        /// <summary>
        /// ��ȡָ�����Եı��ػ��ַ�����
        /// </summary>
        /// <param name="sourceName">���ػ�Դ�����ơ�</param>
        /// <param name="name">��ȡ���ػ��ַ����ļ�����</param>
        /// <param name="culture">���ػ�������Ϣ��</param>
        /// <returns>���ر��ػ��ַ�����</returns>
        public static string GetString(string name, CultureInfo culture, string sourceName)
        {
            return LocalizationManager.Value.GetString(sourceName, name, culture);
        }

        /// <summary>
        /// ��ȡָ�����Եı��ػ��ַ�����
        /// </summary>
        /// <param name="name">��ȡ���ػ��ַ����ļ�����</param>
        /// <param name="culture">���ػ�������Ϣ��</param>
        /// <returns>���ر��ػ��ַ�����</returns>
        public static string GetString(string name, CultureInfo culture)
        {
            return LocalizationManager.Value.GetString(CurrentSourceName, name, culture);
        }

        /// <summary>
        /// ��ȡָ�����Եı��ػ��ַ�����
        /// </summary>
        /// <param name="sourceName">���ػ�Դ�����ơ�</param>
        /// <param name="name">��ȡ���ػ��ַ����ļ�����</param>
        /// <param name="args">��ʽ��������</param>
        /// <returns>���ر��ػ��ַ�����</returns>
        public static string GetString(string sourceName, string name, params object[] args)
        {
            return LocalizationManager.Value.GetString(sourceName, name, args);
        }

        /// <summary>
        /// ��ȡָ�����Եı��ػ��ַ�����
        /// </summary>
        /// <param name="name">��ȡ���ػ��ַ����ļ�����</param>
        /// <param name="args">��ʽ��������</param>
        /// <returns>���ر��ػ��ַ�����</returns>
        public static string GetString(string name, params object[] args)
        {
            return LocalizationManager.Value.GetString(CurrentSourceName, name, args);
        }

        /// <summary>
        ///��ȡָ�����Եı��ػ��ַ�����
        /// </summary>
        /// <param name="sourceName">���ػ�Դ�����ơ�</param>
        /// <param name="name">��ȡ���ػ��ַ����ļ�����</param>
        /// <param name="culture">���ػ�������Ϣ��</param>
        /// <param name="args">��ʽ��������</param>
        /// <returns>���ر��ػ��ַ�����</returns>
        public static string GetString(string sourceName, string name, CultureInfo culture, params object[] args)
        {
            return LocalizationManager.Value.GetString(sourceName, name, culture, args);
        }

        /// <summary>
        /// ��ȡָ�����Եı��ػ��ַ�����
        /// </summary>
        /// <param name="name">��ȡ���ػ��ַ����ļ�����</param>
        /// <param name="culture">���ػ�������Ϣ��</param>
        /// <param name="args">��ʽ��������</param>
        /// <returns>���ر��ػ��ַ�����</returns>
        public static string GetString(string name, CultureInfo culture, params object[] args)
        {
            return LocalizationManager.Value.GetString(CurrentSourceName, name, culture, args);
        }

        /// <summary>
        /// ��ȡ <paramref name="name"/> ��Ӧ�ı��ػ��ַ�����
        /// </summary>
        /// <param name="name">���ػ���Դ�����ơ�</param>
        /// <param name="args">��ʽ��������</param>
        /// <returns>���ر��ػ��ַ�����</returns>
        public static string L(string name, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));

            var value = GetString(name, args);
            return value;
        }

        /// <summary>
        /// ��ȡ <paramref name="name"/> ��Ӧ�ı��ػ��ַ�����
        /// </summary>
        /// <param name="name">���ػ���Դ�����ơ�</param>
        /// <returns>���ر��ػ��ַ�����</returns>
        public static string L(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));

            var value = GetString(name);
            return value;
        }
        /// <summary>
        /// ��ʼ������
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