using BillMonitoring.Client;
using BillMonitoring.Client.Contracts;
using BillMonitoring.Client.Services;
using BillMonitoring.Desktop.ViewModels;
using BillMonitoring.Desktop.ViewModels.Navigator;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BillMonitoring.Desktop
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			IAuthenticationService authenticationService = new AuthenticationService();
			
			IPeriodService periodServise = new PeriodService(authenticationService);
			IBillService billService = new BillService(authenticationService);
			ICategoryService categoryService = new CategoryService(authenticationService);


			LayoutWindow window = new LayoutWindow();

			LayoutViewModel layoutViewModel = new LayoutViewModel();
			ViewModelNavigator viewModelNavigator = new ViewModelNavigator(layoutViewModel);

			LoginViewModel loginViewModel = new LoginViewModel(authenticationService, viewModelNavigator);
			viewModelNavigator.RegisterViewModel(nameof(LoginViewModel), loginViewModel);

			RegistrationViewModel registrationViewModel = new RegistrationViewModel(authenticationService, viewModelNavigator);
			viewModelNavigator.RegisterViewModel(nameof(RegistrationViewModel), registrationViewModel);

			MainViewModel mainViewModel = new MainViewModel(viewModelNavigator, periodServise, categoryService, billService);
			//if (periodServise.GetCurrentPeriod() != null)
			//{
			//	TimeSpan time = periodServise.GetCurrentPeriod().EndDate - DateTime.Now;

			//	if (time.TotalMilliseconds < 0)
			//	{
			//		periodServise.AutoClosePeriod();
			//		mainViewModel.Refresh();
			//	}
			//	else
			//	{
			//		Delay(Convert.ToInt64(time.TotalMilliseconds)).ContinueWith(o =>
			//		{
			//			periodServise.AutoClosePeriod();
			//			mainViewModel.Refresh();
			//		});
			//	}
			//}
			viewModelNavigator.RegisterViewModel(nameof(MainViewModel), mainViewModel);

			SetupPeriodViewModel setupPeriodViewModel = new SetupPeriodViewModel(viewModelNavigator, periodServise);
			viewModelNavigator.RegisterViewModel(nameof(SetupPeriodViewModel), setupPeriodViewModel);

			//PeriodDataViewModel periodDataViewModel = new PeriodDataViewModel(viewModelNavigator, periodManager, billManager, categoryManager);
			//viewModelNavigator.RegisterViewModel(nameof(PeriodDataViewModel), periodDataViewModel);

			CategoriesDataViewModel categoriesDataViewModel = new CategoriesDataViewModel(viewModelNavigator, categoryService);
			viewModelNavigator.RegisterViewModel(nameof(CategoriesDataViewModel), categoriesDataViewModel);

			//BillsViewModel billsViewModel = new BillsViewModel(viewModelNavigator, billManager);
			//viewModelNavigator.RegisterViewModel(nameof(BillsViewModel), billsViewModel);


			base.OnStartup(e);

			layoutViewModel.CurrentViewModel = loginViewModel;
			window.DataContext = layoutViewModel;
			window.Show();
		}

		static async Task Delay(long delay)
		{
			while (delay > 0)
			{
				var currentDelay = delay > int.MaxValue ? int.MaxValue : (int)delay;
				await Task.Delay(currentDelay);
				delay -= currentDelay;
			}
		}
	}
}
