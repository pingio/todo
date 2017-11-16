using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ToDoApp
{
				class CommandViewModel : ICommand
				{
								private Action action;
								private bool canExecute;

								public CommandViewModel(Action act, bool canExecute){
												this.action = act;
												this.canExecute = canExecute;
								}

								public event EventHandler CanExecuteChanged;

								public bool CanExecute(object parameter)
								{
												return canExecute;
								}

								public void Execute(object parameter) => this.action();
				}


}
