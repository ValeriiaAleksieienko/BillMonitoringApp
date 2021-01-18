using BillMonitoring.Client.Contracts;
using BillMonitoring.Desktop.ViewModels.Commands;
using BillMonitoring.Desktop.ViewModels.Navigator;
using BillMonitoring.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BillMonitoring.Desktop.ViewModels
{
	internal class SetupPeriodViewModel : BaseViewModel
	{
		private readonly ViewModelNavigator viewModelNavigator;
		private readonly IPeriodService periodService;

		private PeriodDTO period;

		public SetupPeriodViewModel(ViewModelNavigator viewModelNavigator, IPeriodService periodService)
		{
			this.viewModelNavigator = viewModelNavigator;
			this.periodService = periodService;
			this.Period = null;

			this.ToStartViewCommand = new RelayCommand(() =>
			{
				if (IsPeriodAdding)
				{
					Period = periodService.AddPeriod(StartDate, EndDate, ExpectedAmount);
				}
				else
				{
					periodService.EditPeriod(Period);
				};

				(viewModelNavigator.ViewModels[nameof(MainViewModel)] as MainViewModel)
					.CurrentPeriod = new PeriodViewModel(Period, periodService);

				Period = null;

				viewModelNavigator.NavigateTo(nameof(MainViewModel));
			});
			this.RadioButtonCommand = new RelayCommand<string>(CalculateEndDate);
			this.GoBackCommand = new RelayCommand(() =>
			{
				Period = null;
				viewModelNavigator.NavigateTo(nameof(MainViewModel));
			});
		}

		public PeriodDTO Period
		{
			get { return period; }
			set
			{
				if (value == null)
				{
					IsPeriodAdding = true;
					period = new PeriodDTO();
					StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);
					EndDate = StartDate.AddDays(7).AddSeconds(-1);
					ExpectedAmount = 0;
				}
				else
				{
					IsPeriodAdding = false;
					period = value;
				}
				RaisePropertyChanged(nameof(Period));
			}
		}

		public DateTime StartDate
		{
			get { return Period.StartDate; }
			set
			{
				Period.StartDate = value;
				RaisePropertyChanged(nameof(StartDate));
			}
		}

		public DateTime EndDate
		{
			get { return Period.EndDate; }
			set
			{
				Period.EndDate = value;
				RaisePropertyChanged(nameof(EndDate));
			}
		}

		public double ExpectedAmount
		{
			get { return Period.ExpectedAmount; }
			set
			{
				Period.ExpectedAmount = value;
				RaisePropertyChanged(nameof(ExpectedAmount));
			}
		}

		public bool IsPeriodAdding
		{
			get; set;
		}

		public ICommand ToStartViewCommand
		{
			get;
		}

		public ICommand RadioButtonCommand
		{
			get;
		}

		public ICommand GoBackCommand
		{
			get;
		}

		public void CalculateEndDate(string duration)
		{
			switch (duration)
			{
				case "week":
					EndDate = StartDate.AddDays(7).AddSeconds(-1);
					break;
				case "2 weeks":
					EndDate = StartDate.AddDays(14).AddSeconds(-1);
					break;
				case "month":
					EndDate = StartDate.AddMonths(1).AddSeconds(-1);
					break;
			}
		}
	}
}
