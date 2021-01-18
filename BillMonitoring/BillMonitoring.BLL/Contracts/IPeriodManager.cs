using BillMonitoring.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillMonitoring.BLL.Contracts
{
	public interface IPeriodManager
	{
		Period GetCurrentPeriod(string userId);

		Period AddPeriod(string userId, DateTime startDate, DateTime endDate, double expectedAmount);

		void DeletePeriod(Period period);

		void EditPeriod(Period period);

		void ClosePeriod(Period period);

		IEnumerable<Period> GetClosedPeriods(string userId);

		//double CountTotalAmountByCategory(Period period, Category category);

		void AutoClosePeriod(string userId);

		//double CountTotalAmount(Period period);
	}
}
