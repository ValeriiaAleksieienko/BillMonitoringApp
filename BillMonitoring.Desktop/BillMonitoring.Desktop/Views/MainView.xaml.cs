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
	/// Interaction logic for MainView.xaml
	/// </summary>
	public partial class MainView : UserControl
	{
		public MainView()
		{
			InitializeComponent();
		}

		private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
		{
			ListCurrentPeriod.SelectedIndex = 0;
			ListCurrentPeriod.SelectedItem = (Current.Content as PeriodView).DataContext;
		}
	}
}
