using System;
using System.Windows.Input;

namespace TestApplication {
  /// <summary>
  /// The famous DelegateCommand to allow MVVM style binding of commands, see http://wpftutorial.net/DelegateCommand.html
  /// NOTE: Using this Command you have to call <see cref="RaiseCanExecuteChanged"/> manually to Notify the Command that a changed occured.
  /// </summary>
  public class DelegateCommand : ICommand {
    private readonly Predicate<object> _canExecute;
    private readonly Action<object> _execute;

    public event EventHandler CanExecuteChanged;

    public DelegateCommand(Action<object> execute)
      : this(execute, null) {
    }

    public DelegateCommand(Action<object> execute,
                   Predicate<object> canExecute) {
      this._execute = execute;
      this._canExecute = canExecute;
    }

    public bool CanExecute(object parameter) {
      if (this._canExecute == null) {
        return true;
      }

      return this._canExecute(parameter);
    }

    public void Execute(object parameter) {
      this._execute(parameter);
    }

    public void RaiseCanExecuteChanged() {
      if (this.CanExecuteChanged != null) {
        this.CanExecuteChanged(this, EventArgs.Empty);
      }
    }
  }
}
