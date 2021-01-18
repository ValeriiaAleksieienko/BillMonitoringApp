using BillMonitoring.Client.Contracts;
using BillMonitoring.Desktop.Core;
using BillMonitoring.Desktop.ViewModels.Commands;
using BillMonitoring.Desktop.ViewModels.Navigator;
using BillMonitoring.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace BillMonitoring.Desktop.ViewModels
{
	internal class MainViewModel : BaseViewModel
	{
		private readonly ViewModelNavigator viewModelNavigator;
		private readonly IPeriodService periodService;
		private readonly ICategoryService categoryService;
		//private readonly IBillService billService;

		private PeriodViewModel selectedPeriod;
		private PeriodViewModel currentPeriod;
		private ObservableCollection<PeriodViewModel> closedPeriods;
		private ObservableCollection<CategoryAmount> selectedPeriodInfo;

		private int selectedIndex1 = -1;
		private int selectedIndex2 = -1;

		#region Constructors
		public MainViewModel(ViewModelNavigator viewModelNavigator,
			IPeriodService periodService,
			ICategoryService categoryService,
			IBillService billService)
		{
			this.viewModelNavigator = viewModelNavigator;
			this.periodService = periodService;
			this.categoryService = categoryService;
			//this.billService = billService;

			this.ToSetupViewCommand = new RelayCommand(() =>
			{
				if (CurrentPeriod != null)
				{
					MessageBox.Show("Close current period!", "Error");
				}
				else
				{
					viewModelNavigator.NavigateTo(nameof(SetupPeriodViewModel));
				}
			});
			this.ToCategoriesDataViewCommand = new RelayCommand(() => viewModelNavigator.NavigateTo(nameof(CategoriesDataViewModel)));
			//this.ToBillsViewCommand = new RelayCommand(() => viewModelNavigator.NavigateTo(nameof(BillsViewModel)));

			//SetupClosedPeriods();
			//SetupCurrentPeriod();
		}
		#endregion

		#region Properties

		public ICommand ToSetupViewCommand
		{
			get;
		}
		public ICommand ToCategoriesDataViewCommand
		{
			get;
		}
		public ICommand ToBillsViewCommand
		{
			get;
		}

		public PeriodViewModel CurrentPeriod
		{
			get { return currentPeriod; }
			set
			{
				currentPeriod = value;
				if (value != null)
				{
					currentPeriod.Commands = SetupCurrentPeriodCommands();
					currentPeriod.PeriodFullness = CountPeriodFullness(currentPeriod.Period);
				}
				RaisePropertyChanged(nameof(CurrentPeriod));
				RaisePropertyChanged(nameof(IsCurrentSet));
			}
		}

		public ObservableCollection<PeriodViewModel> ClosedPeriods
		{
			get { return closedPeriods; }
			set
			{
				closedPeriods = value;
				RaisePropertyChanged(nameof(ClosedPeriods));
			}
		}

		public PeriodViewModel SelectedPeriod
		{
			get { return selectedPeriod; }
			set
			{
				if (value != null)
				{
					selectedPeriod = value;
					SelectedPeriodInfo = SetupSelectedPeriodInfo();
				}
				RaisePropertyChanged(nameof(SelectedPeriod));
			}
		}

		public ObservableCollection<CategoryAmount> SelectedPeriodInfo
		{
			get { return selectedPeriodInfo; }
			set
			{
				selectedPeriodInfo = value;
				RaisePropertyChanged(nameof(SelectedPeriodInfo));
			}
		}

		public bool IsCurrentSet
		{
			get { return CurrentPeriod != null; }
		}

		public int SelectedIndex1
		{
			get { return selectedIndex1; }
			set
			{
				selectedIndex1 = value;
				if (selectedIndex1 != -1)
				{
					SelectedIndex2 = -1;
				}
				RaisePropertyChanged(nameof(SelectedIndex1));
			}
		}
		public int SelectedIndex2
		{
			get { return selectedIndex2; }
			set
			{
				selectedIndex2 = value;
				if (selectedIndex2 != -1)
				{
					SelectedIndex1 = -1;
				}
				RaisePropertyChanged(nameof(SelectedIndex2));
			}
		}

		#endregion

		#region Methods

		public void Refresh()
		{
			SetupClosedPeriods();
			ClosedPeriods = closedPeriods;
			SetupCurrentPeriod();
		}

		private void SetupCurrentPeriod()
		{
			PeriodViewModel currentPeriod = new PeriodViewModel(periodService);
			if ((currentPeriod.Period = periodService.GetCurrentPeriod()) != null)
			{
				CurrentPeriod = currentPeriod;
			}
		}

		private void SetupClosedPeriods()
		{
			closedPeriods = new ObservableCollection<PeriodViewModel>();
			foreach (PeriodDTO period in periodService.GetClosedPeriods())
			{
				closedPeriods.Add(new PeriodViewModel(period,
					periodService, CountPeriodFullness(period), SetupClosedPeriodCommands()));
			}
		}

		private ObservableCollection<CategoryAmount> SetupSelectedPeriodInfo()
		{
			ObservableCollection<CategoryAmount> currentPeriodInfo = new ObservableCollection<CategoryAmount>();

			foreach (CategoryDTO category in categoryService.GetCategories())
			{
				currentPeriodInfo.Add(new CategoryAmount
				{
					Category = category,
					Amount = periodService.CountTotalAmountByCategory(selectedPeriod.Period, category)
				});
			}

			return currentPeriodInfo;
		}

		private Dictionary<string, ICommand> SetupCurrentPeriodCommands()
		{
			Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>
			{
				{
					"Delete",
					new RelayCommand(() =>
					{
						periodService.DeletePeriod(CurrentPeriod.Period);

						CurrentPeriod = null;
					})
				},
				{
					"Edit",
					new RelayCommand(() =>
					{
						(viewModelNavigator.ViewModels[nameof(SetupPeriodViewModel)] as SetupPeriodViewModel)
							.Period = CurrentPeriod.Period;
						viewModelNavigator.NavigateTo(nameof(SetupPeriodViewModel));
					})
				},
				{
					"Close",
					new RelayCommand(() =>
					{
						periodService.ClosePeriod(CurrentPeriod.Period);

						CurrentPeriod.Commands = SetupClosedPeriodCommands();
						ClosedPeriods.Insert(0, CurrentPeriod);
						CurrentPeriod = null;
					})
				},
				{
					"Show data",
					new RelayCommand<PeriodViewModel>((period) =>
					{
						//(viewModelNavigator.ViewModels[nameof(PeriodDataViewModel)] as PeriodDataViewModel).Period = period.Period;
						//viewModelNavigator.NavigateTo(nameof(PeriodDataViewModel));
					})
				}
			};

			return commands;
		}

		private Dictionary<string, ICommand> SetupClosedPeriodCommands()
		{
			Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>
			{
				{
					"Delete",
					new RelayCommand<PeriodViewModel>((period) =>
					{
						ClosedPeriods.Remove(period);
						periodService.DeletePeriod(period.Period);
					})
				},
				{
					"Show data",
					new RelayCommand<PeriodViewModel>((period) =>
					{
						//(viewModelNavigator.ViewModels[nameof(PeriodDataViewModel)] as PeriodDataViewModel).Period = period.Period;
						//viewModelNavigator.NavigateTo(nameof(PeriodDataViewModel));
					})
				}
			};

			return commands;
		}

		private PeriodFullness CountPeriodFullness(PeriodDTO period)
		{
			double difference = period.ExpectedAmount - periodService.CountTotalAmount(period);

			return difference switch
			{
				double i when i > 0 => PeriodFullness.Less,
				double i when i == 0 => PeriodFullness.Full,
				_ => PeriodFullness.More,
			};
		}

		#endregion
	}
}
