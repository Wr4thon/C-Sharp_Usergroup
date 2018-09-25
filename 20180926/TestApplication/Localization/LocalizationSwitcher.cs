using System;
using System.Globalization;
using System.Windows;

namespace Localization {
  public class LocalizationSwitcher {
    private readonly LocalizationStorage _localizationStorage;
    private readonly Action<Uri> _publishNewResourceUri;

    /// <inheritdoc />
    public LocalizationSwitcher(LocalizationStorage localizationStorage, Action<Uri> publishNewResourceUri) {
      this._localizationStorage = localizationStorage;
      this._publishNewResourceUri = publishNewResourceUri;
    }

    public void SwitchLanguage(CultureInfo cultureInfo) {
      SwitchLanguage(cultureInfo.Name);
    }

    public void SwitchLanguage(string cultureInfo) {
      if (!this._localizationStorage.TryGetByLanguageCode(cultureInfo, out Uri uri)) {
        uri = this._localizationStorage.DefaultResourceDictionary;
      }

      this._publishNewResourceUri?.Invoke(uri);
    }
  }
}
