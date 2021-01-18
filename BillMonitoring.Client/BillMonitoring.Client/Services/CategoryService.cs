using BillMonitoring.Client.Contracts;
using BillMonitoring.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BillMonitoring.Client.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly IAuthenticationService authenticationService;

		public CategoryService(IAuthenticationService authenticationService)
		{
			this.authenticationService = authenticationService;
		}

		public CategoryDTO AddCategory(string name)
		{
			CategoryDTO categoryDTO = new CategoryDTO
			{
				UserId = authenticationService.UserId,
				Name = name
			};

			var response = authenticationService.Client.PostAsJsonAsync("api/category/addNewCategory", categoryDTO);
			CategoryDTO newCategory;
			if (response.Result.IsSuccessStatusCode)
			{
				newCategory = response.Result.Content.ReadAsAsync<CategoryDTO>().Result;
			}
			else
			{
				newCategory = null;
			}

			return newCategory;
		}

		public async void DeleteCategory(CategoryDTO category)
		{
			await authenticationService.Client.PostAsJsonAsync("api/category/deleteCategory", category);
		}

		public IEnumerable<CategoryDTO> GetCategories()
		{
			var requestMessage = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri(authenticationService.Client.BaseAddress + "api/category/getCategories/"),
				Content = new StringContent(
					JsonConvert.SerializeObject(authenticationService.UserId),
					Encoding.UTF8, "application/json")
			};
			var response = authenticationService.Client.SendAsync(requestMessage).Result;

			return response.Content.ReadAsAsync<IEnumerable<CategoryDTO>>().Result;
		}
	}
}
