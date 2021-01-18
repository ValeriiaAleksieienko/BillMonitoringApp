using BillMonitoring.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillMonitoring.Client.Contracts
{
	public interface ICategoryService
	{
		IEnumerable<CategoryDTO> GetCategories();

		CategoryDTO AddCategory(string name);

		void DeleteCategory(CategoryDTO period);
	}
}
