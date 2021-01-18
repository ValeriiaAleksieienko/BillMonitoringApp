using BillMonitoring.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillMonitoring.Client.Contracts
{
	public interface IPeriodService
	{
		double CountTotalAmount(PeriodDTO period);

		IEnumerable<PeriodDTO> GetClosedPeriods();

		PeriodDTO GetCurrentPeriod();

		double CountTotalAmountByCategory(PeriodDTO period, CategoryDTO category);

		void DeletePeriod(PeriodDTO period);

		void ClosePeriod(PeriodDTO period);

		PeriodDTO AddPeriod(DateTime StartDate, DateTime EndDate, double ExpectedAmount);

		void EditPeriod(PeriodDTO period);

		void AutoClosePeriod();
	}
}
