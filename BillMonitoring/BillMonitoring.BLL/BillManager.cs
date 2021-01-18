using BillMonitoring.BLL.Contracts;
using BillMonitoring.DAL.Contracts;
using BillMonitoring.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillMonitoring.BLL
{
	public class BillManager : IBillManager
	{
		private readonly IUnitOfWork unitOfWork;

		public BillManager(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public Bill AddBill(string userId, DateTime date, double amount, string info, Category category)
		{
			Bill bill = new Bill
			{
				Id = Guid.NewGuid(),
				User = unitOfWork.Users.First(u => u.Id == userId),
				Date = date,
				Amount = amount,
				Info = info,
				Category = category
			};
			unitOfWork.BillRepository.Create(bill);
			unitOfWork.Save();

			return bill;
		}

		public void DeleteBill(Bill bill)
		{
			unitOfWork.BillRepository.Delete(bill.Id.ToString());
			unitOfWork.Save();
		}

		public void EditBill(Bill bill)
		{
			unitOfWork.BillRepository.Update(bill);
			unitOfWork.Save();
		}

		public void EditBillsCategory(string userId, Category oldCategory, Category newCategory)
		{
			IEnumerable<Bill> bills = GetAllBills(userId).Where(b => b.Category == oldCategory);
			foreach(Bill bill in bills)
			{
				bill.Category = newCategory;
				EditBill(bill);
			}
		}

		public IEnumerable<Bill> GetAllBills(string userId)
		{
			return unitOfWork.BillRepository.GetAll().Where(b => b.User.Id == userId);
		}

		public IEnumerable<Bill> GetBillByPeriod(Guid periodId)
		{
			return unitOfWork.BillRepository.GetAll().Where(b => b.Period.Id==periodId);
		}

		public IEnumerable<Bill> GetBillsForDatesRange(string userId, DateTime startDate, DateTime endDate)
		{
			return unitOfWork.BillRepository.GetAll().Where(b => b.Date>= startDate && b.Date <= endDate);
		}

		public DateTime GetDateOfErliesttBill(string userId)
		{
			DateTime date;

			return (date = unitOfWork.BillRepository.GetAll().Min(b => b.Date)) != null ? date : DateTime.Today;
		}

		public DateTime GetDateOfLatestBill(string userId)
		{
			DateTime date;

			return (date = unitOfWork.BillRepository.GetAll().Max(b => b.Date))!= null ? date : DateTime.Today;
		}
	}
}
