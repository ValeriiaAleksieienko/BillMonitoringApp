using BillMonitoring.BLL.Contracts;
using BillMonitoring.DAL.Contracts;
using BillMonitoring.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillMonitoring.BLL
{
	public class CategoryManager : ICategoryManager
	{
		private readonly IUnitOfWork unitOfWork;

		public CategoryManager(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public Category AddCategory(string userId, string name)
		{
			Category category = new Category
			{
				Id = Guid.NewGuid(),
				Name = name,
				User = unitOfWork.Users.First(u => u.Id == userId)
			};
			unitOfWork.CategoryRepository.Create(category);
			unitOfWork.Save();
			return category;
		}

		public IEnumerable<Category> GetCategories(string userId)
		{
			return unitOfWork.CategoryRepository.GetAll().Where(c => c.User.Id == userId);
		}

		public void DeleteCategory(Category category)
		{
			unitOfWork.CategoryRepository.Delete(category.Id.ToString());
			unitOfWork.Save();
		}

		public void EditCategory(Category category)
		{
			unitOfWork.CategoryRepository.Update(category);
			unitOfWork.Save();
		}

		public void SeedCategoriesForNewUser(string userId)
		{
			AddCategory(userId, "Food");
			AddCategory(userId, "Medicine");
			AddCategory(userId, "HouseholdGoods");
			AddCategory(userId, "Clothes");
			AddCategory(userId, "Medicine");
			AddCategory(userId, "Other");
		}
	}
}
