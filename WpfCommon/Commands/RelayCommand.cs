using System;
using System.Windows.Input;

namespace WpfCommon.Commands {
	public class RelayCommand : ICommand {
		private Action _execute;
		private Func<bool> _canExecute;

		public event EventHandler CanExecuteChanged {
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		protected RelayCommand() {

		}

		public RelayCommand(Action execute)
			: this(execute, null) {

		}
		public RelayCommand(Action execute, Func<bool> canExecute) {
			_execute = execute ?? throw new ArgumentNullException(nameof(execute));
			_canExecute = canExecute;
		}

		public virtual void Execute(object parameter) {
			_execute();
		}

		public virtual bool CanExecute(object parameter) {
			return _canExecute?.Invoke() ?? true;
		}
	}

	public class RelayCommand<T> : RelayCommand {
		private Action<T> _execute;
		private Func<T, bool> _canExecute;

		public RelayCommand(Action<T> execute)
			: this(execute, null) {
		}

		public RelayCommand(Action<T> execute, Func<T, bool> canExecute) {
			_execute = execute ?? throw new ArgumentNullException(nameof(execute));
			_canExecute = canExecute;
		}

		public override void Execute(object parameter) {
			_execute((T)parameter);
		}

		public override bool CanExecute(object parameter) {
			return _canExecute?.Invoke((T)parameter) ?? true;
		}
	}
}
