using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;

using Localization;

namespace TestApplication {
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application {
    private readonly LocalizationSwitcher _localizationSwitcher;
    private readonly LocalizationStorage _storage;
    private CultureInfo _currentLanguage;
    private const string LanguageFile = ".\\.language";

    /// <inheritdoc />
    public App() {
      this._storage = new LocalizationStorage();
      this._localizationSwitcher = new LocalizationSwitcher(this._storage, SetNewResourceDictionary);
    }

    public void SetNewResourceDictionary(Uri uri) {
      ResourceDictionary dict = new ResourceDictionary();
      dict.Source = uri;
      this.Resources.MergedDictionaries.Add(dict);
    }

    internal void ChangeLanguage(CultureInfo language) {
      this._currentLanguage = language;
      this._localizationSwitcher.SwitchLanguage(language);
    }


    /// <inheritdoc />
    protected override void OnStartup(StartupEventArgs e) {
      base.OnStartup(e);
      SetupLocalizationStorage();

      if (!TryReadCultureInfoFromFile(out CultureInfo cultureInfo)) {
        cultureInfo = Thread.CurrentThread.CurrentCulture;
      }

      ChangeLanguage(cultureInfo);
    }

    private bool TryReadCultureInfoFromFile(out CultureInfo cultureInfo) {
      cultureInfo = null;
      if (!File.Exists(LanguageFile)) {
        return false;
      }

      using (FileStream fs = new FileStream(LanguageFile, FileMode.Open, FileAccess.Read)) {
        using (StreamReader sr = new StreamReader(fs)) {
          string content = sr.ReadLine();
          foreach (CultureInfo culture in CultureInfo.GetCultures(CultureTypes.AllCultures)) {
            if (string.Compare(culture.Name, content, StringComparison.Ordinal) != 0) {
              continue;
            }

            cultureInfo = culture;
            return true;
          }

          return false;
        }
      }
    }

    private void SetupLocalizationStorage() {
      Uri english = new Uri("Localization\\Localization.xaml", UriKind.Relative);
      this._storage.DefaultResourceDictionary = english;
      this._storage.Add("en-US", english);
      this._storage.Add("de-DE", new Uri("Localization\\Localization.de-DE.xaml", UriKind.Relative));
    }

    protected override void OnExit(ExitEventArgs e) {
      using (FileStream fs = new FileStream(LanguageFile, FileMode.Create, FileAccess.Write)) {
        using (StreamWriter sw = new StreamWriter(fs)) {
          sw.Write(this._currentLanguage.ToString());
        }
      }
      base.OnExit(e);
    }

    public ICollection<CultureInfo> GetAvailableLanguages() {
      return new CultureInfo[] {
        CultureInfo.GetCultureInfo("en-US"),
        CultureInfo.GetCultureInfo("de-DE"),
      };
    }

    public CultureInfo GetCurrentLanguage() {
      return this._currentLanguage;
    }
  }
}
