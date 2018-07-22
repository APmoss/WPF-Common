using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfCommon.Foundations {
	public abstract class ObservableBase : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;

		public bool Set<T>(ref T property, T value, [CallerMemberName] string propertyName = null) {
			if (propertyName == null) {
				throw new ArgumentNullException(nameof(propertyName));
			}

			if (!EqualityComparer<T>.Default.Equals(property, value)) {
				property = value;
				NotifyPropertyChanged(propertyName);

				return true;
			}

			return false;
		}

		public void NotifyPropertyChanged([CallerMemberName] string propertyName = null) {
			if (propertyName == null) {
				throw new ArgumentNullException(nameof(propertyName));
			}

			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
