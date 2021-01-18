using BillMonitoring.DAL.Contracts;
using BillMonitoring.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillMonitoring.DAL.Repositories
{
	public class BillRepository : IRepository<Bill>
	{
		private readonly ApplicationDbContext dbContext;

		public BillRepository(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public void Create(Bill item)
		{
			dbContext.Bills.Add(item);
		}

		public void Delete(string id)
		{
			Bill bill = GetOne(id);
			if(bill != null)
			{
				dbContext.Bills.Remove(bill);
			}
		}

		public IEnumerable<Bill> GetAll()
		{
			return dbContext.Bills.Include(b => b.User).Include(b => b.Period).Include(b => b.Category);
		}

		public Bill GetOne(string id)
		{
			return GetAll().First(b => b.Id.ToString() == id);
		}

		public void Update(Bill item)
		{
			dbContext.Entry(item).State = EntityState.Modified;
		}
	}
}
