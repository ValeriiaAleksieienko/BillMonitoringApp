using BillMonitoring.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace BillMonitoring.Desktop
{
	public class PeriodFullnesToColorBrushConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			switch ((PeriodFullness)value)
			{
				case PeriodFullness.Less:
					return new SolidColorBrush(Color.FromRgb(102, 255, 51));
				case PeriodFullness.Full:
					return new SolidColorBrush(Color.FromRgb(21, 117, 11));
				case PeriodFullness.More:
					return new SolidColorBrush(Color.FromRgb(230, 0, 0));
				default:
					return new SolidColorBrush(Colors.Black);
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return DependencyProperty.UnsetValue;
		}
	}
}
