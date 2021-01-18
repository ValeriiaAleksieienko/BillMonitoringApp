using BillMonitoring.BLL.Contracts;
using BillMonitoring.DTO;
using BillMonitoring.Entities;
using BillMonitoring.WebAPI.AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillMonitoring.WebAPI.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryManager categoryManager;
		private readonly BMAutoMapper mapper;

		public CategoryController(ICategoryManager categoryManager, BMAutoMapper mapper)
		{
			this.categoryManager = categoryManager;
			this.mapper = mapper;
		}

		[HttpPost]
		[Route("addNewCategory")]
		public IActionResult AddNewPeriod([FromBody] CategoryDTO categoryDTO)
		{

			Category category = categoryManager.AddCategory(categoryDTO.UserId, categoryDTO.Name);
			CategoryDTO newCategory = mapper.Map<Category, CategoryDTO>(category);
			return Ok(newCategory);
		}

		[HttpPut]
		[Route("editCategory")]
		public IActionResult EditPeriod([FromBody] CategoryDTO categoryDTO)
		{
			Category category = mapper.Map<CategoryDTO, Category>(categoryDTO);
			categoryManager.EditCategory(category);
			return Ok();
		}

		[HttpPost]
		[Route("deleteCategory")]
		public IActionResult DeletePeriod([FromBody] CategoryDTO categoryDTO)
		{
			Category category = mapper.Map<CategoryDTO, Category>(categoryDTO);
			categoryManager.DeleteCategory(category);
			return Ok();
		}

		[HttpGet]
		[Route("getCategories")]
		public IActionResult GetClosedPeriods([FromBody] string userId)
		{
			IEnumerable<Category> categories = categoryManager.GetCategories(userId);
			IEnumerable<CategoryDTO> categoryDTOs = mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(categories);
			return Ok(categoryDTOs);
		}
	}
}
