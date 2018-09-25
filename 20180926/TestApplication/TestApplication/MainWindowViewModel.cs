using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using TestApplication.Annotations;

namespace TestApplication {
  public class MainWindowViewModel : INotifyPropertyChanged {
    private CultureInfo _selectedLanguage;
    private readonly App _app;

    public ICollection<CultureInfo> AvailableLanguages { get; set; }

    public ObservableCollection<City> Cities { get; set; }

    public CultureInfo SelectedLanguage {
      get { return this._selectedLanguage; }
      set {
        this._selectedLanguage = value;
        this._app.ChangeLanguage(value);
        OnPropertyChanged();
      }
    }

    /// <inheritdoc />
    public MainWindowViewModel() {
      CitiesLoader loader = new CitiesLoader();
      this._app = ((App)Application.Current);
      this.AvailableLanguages = this._app.GetAvailableLanguages();
      this.SelectedLanguage = this._app.GetCurrentLanguage();
      this.Cities = new ObservableCollection<City>(loader.Load());
    }

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
      this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
