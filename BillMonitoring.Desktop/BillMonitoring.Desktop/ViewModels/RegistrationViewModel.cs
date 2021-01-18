using BillMonitoring.Client.Contracts;
using BillMonitoring.Desktop.ViewModels.Commands;
using BillMonitoring.Desktop.ViewModels.Navigator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BillMonitoring.Desktop.ViewModels
{
	internal class RegistrationViewModel : BaseViewModel
	{
		private string userName;
		private string email;

		public RegistrationViewModel(IAuthenticationService authenticationService, ViewModelNavigator viewModelMavigator)
		{
			this.RegisterCommand = new RelayCommand<object>(async (password) =>
			{
				var response = await authenticationService.Register(new { userName, email, (password as PasswordBox).Password });
				
				MessageBox.Show(response["message"].ToString(), response["status"].ToString());
				if (response["status"].ToString() == "Success")
				{
					viewModelMavigator.NavigateTo(nameof(LoginViewModel));
				}
				
			});
			this.ToLoginViewCommand = new RelayCommand(() => viewModelMavigator.NavigateTo(nameof(LoginViewModel)));
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

		public string Email
		{
			get { return email; }
			set
			{
				email = value;
				RaisePropertyChanged(nameof(Email));
			}
		}

		public ICommand RegisterCommand { get; }

		public ICommand ToLoginViewCommand { get; }
	}
}
