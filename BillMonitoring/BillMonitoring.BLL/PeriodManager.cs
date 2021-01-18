using BillMonitoring.BLL.Contracts;
using BillMonitoring.DAL.Contracts;
using BillMonitoring.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillMonitoring.BLL
{
	public class PeriodManager : IPeriodManager
	{
		private readonly IUnitOfWork unitOfWork;

		public PeriodManager(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public Period AddPeriod(string userId, DateTime startDate, DateTime endDate, double expectedAmount)
		{
			Period period = new Period
			{
				Id = Guid.NewGuid(),
				User = unitOfWork.Users.First(u => u.Id == userId),
				StartDate = startDate,
				EndDate = endDate,
				ExpectedAmount = expectedAmount,
				IsClosed = false,
				Bills = new List<Bill>()
			};
			unitOfWork.PeriodRepository.Create(period);
			unitOfWork.Save();

			return period;
		}

		public void AutoClosePeriod(string userId)
		{
			Period period = GetCurrentPeriod(userId);
			period.IsClosed = true;
			unitOfWork.PeriodRepository.Update(period);
			unitOfWork.Save();

			int periodDuration = (period.EndDate - period.StartDate).Days + 1;
			AddPeriod(userId, period.EndDate.AddDays(1), period.EndDate.AddDays(periodDuration), period.ExpectedAmount);
		}

		public void ClosePeriod(Period period)
		{
			period.EndDate = DateTime.Today;
			period.IsClosed = true;
			unitOfWork.PeriodRepository.Update(period);
			unitOfWork.Save();
		}

		public void DeletePeriod(Period period)
		{
			unitOfWork.PeriodRepository.Delete(period.Id.ToString());
			unitOfWork.Save();
		}

		public void EditPeriod(Period period)
		{
			unitOfWork.PeriodRepository.Update(period);
			unitOfWork.Save();
		}

		public IEnumerable<Period> GetClosedPeriods(string userId)
		{
			return unitOfWork.PeriodRepository.GetAll().Where(p => p.User.Id == userId && p.IsClosed == true);
		}

		public Period GetCurrentPeriod(string userId)
		{
			return unitOfWork.PeriodRepository.GetAll().FirstOrDefault(p => p.User.Id == userId && p.IsClosed == false);
		}
	}
}
