using System;
using System.Collections.Generic;
using System.Text;

namespace BillMonitoring.Desktop.ViewModels.Navigator
{
	internal class ViewModelNavigator
	{
		private LayoutViewModel layoutViewModel;
		private Dictionary<string, BaseViewModel> viewModels;

		public ViewModelNavigator(LayoutViewModel layoutViewModel)
		{
			this.layoutViewModel = layoutViewModel;
			this.viewModels = new Dictionary<string, BaseViewModel>();
		}

		public Dictionary<string, BaseViewModel> ViewModels
		{
			get { return viewModels; }
		}

		public void RegisterViewModel(string key, BaseViewModel baseViewModel)
		{
			if (!viewModels.ContainsKey(key))
			{
				viewModels.Add(key, baseViewModel);
			}
		}

		public void NavigateTo(string viewModel)
		{
			if (viewModels.ContainsKey(viewModel))
			{
				layoutViewModel.CurrentViewModel = viewModels[viewModel];
			}

		}
	}
}
