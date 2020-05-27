using System.Collections.Generic;

namespace MC.Common.Localization.Dictionaries
{
    public interface ILocalizationDictionaryProvider
    {
        ILocalizationDictionary DefaultDictionary { get; }

        IDictionary<string, ILocalizationDictionary> Dictionaries { get; }

        void Initialize(string sourceName);

        void Extend(ILocalizationDictionary dictionary);
    }
}