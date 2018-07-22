using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfCommon.Commands {
	public class AsyncRelayCommand : ICommand {
		private Func<Task> _execute;
		private Func<bool> _canExecute;

		public event EventHandler CanExecuteChanged {
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		protected AsyncRelayCommand() {

		}

		public AsyncRelayCommand(Func<Task> execute)
			: this(execute, null) {

		}
		public AsyncRelayCommand(Func<Task> execute, Func<bool> canExecute) {
			_execute = execute ?? throw new ArgumentNullException(nameof(execute));
			_canExecute = canExecute;
		}

		public virtual async void Execute(object parameter) {
			await _execute();
		}

		public virtual bool CanExecute(object parameter) {
			return _canExecute?.Invoke() ?? true;
		}
	}

	public class AsyncRelayCommand<T> : AsyncRelayCommand {
		private Func<T, Task> _execute;
		private Func<T, bool> _canExecute;

		public AsyncRelayCommand(Func<T, Task> execute)
			: this(execute, null) {

		}
		public AsyncRelayCommand(Func<T, Task> execute, Func<T, bool> canExecute) {
			_execute = execute ?? throw new ArgumentNullException(nameof(execute));
			_canExecute = canExecute;
		}

		public override async void Execute(object parameter) {
			await _execute((T)parameter);
		}

		public override bool CanExecute(object parameter) {
			return _canExecute?.Invoke((T)parameter) ?? true;
		}
	}
}
