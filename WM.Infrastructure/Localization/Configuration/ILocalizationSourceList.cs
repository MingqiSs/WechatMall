using System.Collections.Generic;
using MC.Common.Localization.Sources;

namespace MC.Common.Localization.Configuration
{
    /// <summary>
    /// Defines a specialized list to store <see cref="ILocalizationSource"/> object.
    /// </summary>
    public interface ILocalizationSourceList : IList<ILocalizationSource>
    {
        /// <summary>
        /// Extensions for dictionay based localization sources.
        /// </summary>
        IList<LocalizationSourceExtensionInfo> Extensions { get; }
    }
}