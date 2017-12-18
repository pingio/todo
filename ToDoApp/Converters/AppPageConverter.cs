using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp
{
	class AppPageConverter : BaseValueConverter<AppPageConverter>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			switch ((AppPage)value)
			{
				default:
					Debugger.Break();
					return null;

				case AppPage.Main:
					return new MainPage();

				case AppPage.Settings:
					return new SettingsPage();
			}
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
