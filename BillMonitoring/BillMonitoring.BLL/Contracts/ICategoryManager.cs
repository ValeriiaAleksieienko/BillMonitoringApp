using BillMonitoring.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillMonitoring.BLL.Contracts
{
	public interface ICategoryManager
	{
		IEnumerable<Category> GetCategories(string userId);

		Category AddCategory(string userId, string name);

		void EditCategory(Category category);

		void DeleteCategory(Category category);

		void SeedCategoriesForNewUser(string userId);
	}
}
