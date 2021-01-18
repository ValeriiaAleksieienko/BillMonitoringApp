using BillMonitoring.DAL.Repositories;
using BillMonitoring.Entities;
using BillMonitoring.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillMonitoring.DAL.Contracts
{
	public interface IUnitOfWork : IDisposable
	{
		IEnumerable<ApplicationUser> Users { get; }

		IRepository<Period> PeriodRepository { get; }

		IRepository<Bill> BillRepository { get; }

		IRepository<Category> CategoryRepository { get; }

		void Save();
	}
}
