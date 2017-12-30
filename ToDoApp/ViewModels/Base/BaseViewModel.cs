using PropertyChanged;
using System.ComponentModel;

namespace ToDoApp
{
	[AddINotifyPropertyChangedInterface]
	class BaseViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

		public BaseViewModel ViewModel { get; set; }
	}
}
