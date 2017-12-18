using System.Windows;
using System;
namespace ToDoApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			WindowViewModel wvm = new WindowViewModel();

			this.DataContext = wvm;

		}
	}
}