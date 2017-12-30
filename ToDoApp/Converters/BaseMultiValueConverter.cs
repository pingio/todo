using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace ToDoApp
{
	abstract class BaseMultiValueConverter<T> : MarkupExtension, IMultiValueConverter where T : class, new()

	{

		private static T converter = null;

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return converter ?? (converter = new T());
		}

		public abstract object Convert(object[] values, Type targetType, object parameter, CultureInfo culture);

		public abstract object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture);
	}
}
