using ToDoApp;
using System.Windows;
using System.Windows.Controls;

namespace ToDoApp
{
	/// <summary>
	/// Interaction logic for MainPage.xaml
	/// </summary>
	public partial class MainPage : Page
	{
		public MainPage()
		{
			this.DataContext = new MainViewModel();
			InitializeComponent();
		}
	}
}
