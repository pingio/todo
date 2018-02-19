using System.Diagnostics;
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
			var mwm = new MainViewModel();
			this.DataContext = mwm;

			WindowViewModel.Instance.CurrentViewModel = mwm;
			InitializeComponent();
		}
	}
}
