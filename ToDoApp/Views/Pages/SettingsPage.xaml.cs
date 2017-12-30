
using System.Windows.Controls;

namespace ToDoApp
{
	/// <summary>
	/// Interaction logic for SettingsPage.xaml
	/// </summary>
	public partial class SettingsPage : Page
	{
		public SettingsPage()
		{
			var swm = new SettingsViewModel();
			this.DataContext = swm;
			InitializeComponent();
		}
	}
}
