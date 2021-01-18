using BillMonitoring.DAL.Contracts;
using BillMonitoring.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillMonitoring.DAL.Repositories
{
	public class PeriodRepository : IRepository<Period>
	{
		private readonly ApplicationDbContext dbContext;

		public PeriodRepository(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public void Create(Period item)
		{
			dbContext.Periods.Add(item);
		}

		public void Delete(string id)
		{
			Period period = GetOne(id);
			if(period != null)
			{
				dbContext.Periods.Remove(period);
			}
		}

		public IEnumerable<Period> GetAll()
		{
			return dbContext.Periods.Include(p => p.User).Include(p => p.Bills);
		}

		public Period GetOne(string id)
		{
			return GetAll().First(p => p.Id.ToString() == id);
		}

		public void Update(Period item)
		{
			dbContext.Entry(item).State = EntityState.Modified;
		}
	}
}
