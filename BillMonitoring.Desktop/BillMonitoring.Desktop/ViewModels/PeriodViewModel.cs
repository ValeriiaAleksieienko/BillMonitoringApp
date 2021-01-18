using BillMonitoring.Client.Contracts;
using BillMonitoring.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BillMonitoring.Desktop.ViewModels
{
	internal class PeriodViewModel : BaseViewModel
	{
		private readonly IPeriodService periodService;

		private PeriodDTO period;
		private PeriodFullness periodFullness;
		private Dictionary<string, ICommand> commands;

		public PeriodViewModel(PeriodDTO period, IPeriodService periodService, PeriodFullness periodFullness,
			Dictionary<string, ICommand> commands) : this(period, periodService)
		{
			this.periodFullness = periodFullness;
			this.commands = commands;
		}

		public PeriodViewModel(PeriodDTO period, IPeriodService periodService) : this(periodService)
		{
			this.period = period;
		}

		public PeriodViewModel(IPeriodService periodService)
		{
			this.periodService = periodService;
		}

		public PeriodDTO Period
		{
			get { return period; }
			set
			{
				period = value;
				RaisePropertyChanged(nameof(Period));
			}
		}

		public double PeriodTotalAmount
		{
			get { return periodService.CountTotalAmount(Period); }
			set { }
		}

		public PeriodFullness PeriodFullness
		{
			get { return periodFullness; }
			set
			{
				periodFullness = value;
				RaisePropertyChanged(nameof(PeriodFullness));
			}
		}

		public Dictionary<string, ICommand> Commands
		{
			get { return commands; }
			set
			{
				commands = value;
				RaisePropertyChanged(nameof(Commands));
			}
		}

	}

	public enum PeriodFullness
	{
		Less,
		Full,
		More
	}
}
