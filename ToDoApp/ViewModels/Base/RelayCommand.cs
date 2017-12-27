using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ToDoApp
{
	class RelayCommand : ICommand
	{
		private Action<object> action;
		private bool canExecute;

		public RelayCommand(Action<object> act, bool canExecute)
		{
			this.action = act;
			this.canExecute = canExecute;
		}

		public event EventHandler CanExecuteChanged = (sender, e) => { };

		public bool CanExecute(object parameter)
		{
			return canExecute;
		}

		public void Execute(object parameter) => this.action(parameter);
	}


}
