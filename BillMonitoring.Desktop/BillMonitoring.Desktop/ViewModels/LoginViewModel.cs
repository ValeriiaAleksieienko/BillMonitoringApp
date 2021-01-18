using BillMonitoring.Client;
using BillMonitoring.Client.Contracts;
using BillMonitoring.Desktop.ViewModels.Commands;
using BillMonitoring.Desktop.ViewModels.Navigator;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BillMonitoring.Desktop.ViewModels
{
	internal class LoginViewModel : BaseViewModel
	{
		private string userName;
		public LoginViewModel(IAuthenticationService authenticationService, ViewModelNavigator viewModelMavigator)
		{
			this.LogInCommand = new RelayCommand<object>(async(password) =>
			{
				if(await authenticationService.Login(new { userName, (password as PasswordBox).Password }))
				{
					(viewModelMavigator.ViewModels[nameof(MainViewModel)] as MainViewModel).Refresh();
					(viewModelMavigator.ViewModels[nameof(CategoriesDataViewModel)] as CategoriesDataViewModel).Refresh();
					viewModelMavigator.NavigateTo(nameof(MainViewModel));
				}
				else
				{
					MessageBox.Show("Wrong User name or Password!", "Unauthorized");
				}
			});
			this.ToRegistrationViewCommand = new RelayCommand(() => viewModelMavigator.NavigateTo(nameof(RegistrationViewModel)));
		}

		public string UserName 
		{ 
			get { return userName; }
			set
			{
				userName = value;
				RaisePropertyChanged(nameof(UserName));
			}
		}

		public ICommand LogInCommand { get; }

		public ICommand ToRegistrationViewCommand { get; }
	}
}
