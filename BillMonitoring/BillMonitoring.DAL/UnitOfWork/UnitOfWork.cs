using BillMonitoring.DAL.Contracts;
using BillMonitoring.DAL.Repositories;
using BillMonitoring.Entities;
using BillMonitoring.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillMonitoring.DAL.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext dbContext;

		private IRepository<Period> periodRepository;
		private IRepository<Bill> billRepository;
		private IRepository<Category> categoryRepository;

		private bool disposed = false;

		public UnitOfWork(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public IEnumerable<ApplicationUser> Users
		{
			get { return dbContext.Users; }
		}

		public IRepository<Period> PeriodRepository 
		{
			get
			{
				if (periodRepository == null)
					periodRepository = new PeriodRepository(dbContext);
				return periodRepository;
			}
		}

		public IRepository<Bill> BillRepository
		{
			get
			{
				if (billRepository == null)
					billRepository = new BillRepository(dbContext);
				return billRepository;
			}
		}

		public IRepository<Category> CategoryRepository
		{
			get
			{
				if (categoryRepository == null)
					categoryRepository = new CategoryRepository(dbContext);
				return categoryRepository;
			}
		}

		public void Save()
		{
			dbContext.SaveChanges();
		}

		private void CleanUp(bool disposing)
		{
			if (!disposed)
			{
				if (disposing) { }
				
				dbContext.Dispose();
				disposed = true;
			}
		}

		public void Dispose()
		{
			CleanUp(true);
			GC.SuppressFinalize(this);
		}

		~UnitOfWork()
		{
			CleanUp(false);
		}
	}
}
