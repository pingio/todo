using System;
using System.Globalization;

namespace ToDoApp
{
	class MultiCommandParameterConverter : BaseMultiValueConverter<MultiCommandParameterConverter>
	{
		public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			return values.Clone();
		}

		public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
