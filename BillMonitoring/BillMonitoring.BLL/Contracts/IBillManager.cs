using BillMonitoring.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillMonitoring.BLL.Contracts
{
	public interface IBillManager
	{
		Bill AddBill(string userId, DateTime date, double amount, string info, Category category);

		IEnumerable<Bill> GetAllBills(string userId);

		IEnumerable<Bill> GetBillByPeriod(Guid periodId);

		IEnumerable<Bill> GetBillsForDatesRange(string userId, DateTime startDate, DateTime endDate);

		void EditBillsCategory(string userId, Category oldCategory, Category newCategory);

		void DeleteBill(Bill bill);

		DateTime GetDateOfErliesttBill(string userId);

		DateTime GetDateOfLatestBill(string userId);

		void EditBill(Bill bill);
	}
}
