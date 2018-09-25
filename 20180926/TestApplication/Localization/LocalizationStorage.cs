using System;
using System.Collections.Generic;
using System.Globalization;

namespace Localization {
  public class LocalizationStorage {
    private readonly IDictionary<string, Uri> _storage;
    public Uri DefaultResourceDictionary { get; set; }

    /// <inheritdoc />
    public LocalizationStorage() {
      this._storage = new Dictionary<string, Uri>();
    }

    public bool TryGetByLanguageCode(string languageCode, out Uri resourceDirectoryPath) {
      return this._storage.TryGetValue(languageCode, out resourceDirectoryPath);
    }

    public void Add(string languageCode, Uri resourceDirectoryPath) {
      this._storage.Add(languageCode, resourceDirectoryPath);
    }

    public void Add(CultureInfo languageCode, Uri resourceDirectoryPath) {
      Add(languageCode.Name, resourceDirectoryPath);
    }
  }
}