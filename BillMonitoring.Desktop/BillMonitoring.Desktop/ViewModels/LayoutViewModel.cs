using System;
using System.Collections.Generic;
using System.Text;

namespace BillMonitoring.Desktop.ViewModels
{
	internal class LayoutViewModel : BaseViewModel
	{
		private BaseViewModel currentViewModel;

		public BaseViewModel CurrentViewModel
		{
			get { return currentViewModel; }
			set
			{
				currentViewModel = value;
				RaisePropertyChanged(nameof(CurrentViewModel));
			}
		}
	}
}
