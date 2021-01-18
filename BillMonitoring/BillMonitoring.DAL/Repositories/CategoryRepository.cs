using BillMonitoring.DAL.Contracts;
using BillMonitoring.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillMonitoring.DAL.Repositories
{
	public class CategoryRepository : IRepository<Category>
	{
		private readonly ApplicationDbContext dbContext;

		public CategoryRepository(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public void Create(Category item)
		{
			dbContext.Categories.Add(item);
		}

		public void Delete(string id)
		{
			Category category = GetOne(id);
			if(category != null)
			{
				dbContext.Categories.Remove(category);
			}
		}

		public IEnumerable<Category> GetAll()
		{
			return dbContext.Categories.Include(c => c.User);
		}

		public Category GetOne(string id)
		{
			return GetAll().First(b => b.Id.ToString() == id);
		}

		public void Update(Category item)
		{
			dbContext.Entry(item).State = EntityState.Modified;
		}
	}
}
