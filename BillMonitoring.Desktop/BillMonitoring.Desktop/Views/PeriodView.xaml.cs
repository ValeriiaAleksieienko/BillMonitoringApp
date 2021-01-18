using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BillMonitoring.Desktop.Views
{
	/// <summary>
	/// Interaction logic for PeriodView.xaml
	/// </summary>
	public partial class PeriodView : UserControl
	{
		public PeriodView()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Menu.IsOpen = true;
		}
	}
}
