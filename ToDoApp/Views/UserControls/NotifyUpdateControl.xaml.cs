using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace ToDoApp
{
	/// <summary>
	/// Interaction logic for NotifyUpdateControl.xaml
	/// </summary>
	public partial class NotifyUpdateControl : UserControl
	{

		public NotifyUpdateControl()
		{
			InitializeComponent();
			this.Loaded += IsLoaded;
		}


		public new async void IsLoaded(object sender, RoutedEventArgs e)
		{
			await CheckForUpdate();
		}

		private async Task CheckForUpdate()
		{
			await Task.Delay(1000);
			var updateCheck = new UpdateChecker();

			if (updateCheck.HasUpdate())
			{
				var sb = new Storyboard();
				var animation = new ThicknessAnimation
				{
					Duration = new Duration(TimeSpan.FromSeconds(0.3f)),
					To = new Thickness(0)
				};

				Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
				sb.Children.Add(animation);
				sb.Begin(this);
			}
		}
	}
}
