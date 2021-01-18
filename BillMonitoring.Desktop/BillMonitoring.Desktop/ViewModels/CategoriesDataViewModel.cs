using BillMonitoring.Client.Contracts;
using BillMonitoring.Desktop.ViewModels.Commands;
using BillMonitoring.Desktop.ViewModels.Navigator;
using BillMonitoring.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace BillMonitoring.Desktop.ViewModels
{
	internal class CategoriesDataViewModel : BaseViewModel
	{
		private readonly ViewModelNavigator viewModelNavigator;
		private readonly ICategoryService categoryService;

		private string categoryName;

		public CategoriesDataViewModel(ViewModelNavigator viewModelNavigator, ICategoryService categoryService)
		{
			this.viewModelNavigator = viewModelNavigator;
			this.categoryService = categoryService;

			this.GoBackCommand = new RelayCommand(() => viewModelNavigator.NavigateTo(nameof(MainViewModel)));
			this.AddCategoryCommand = new RelayCommand(() =>
			{
				CategoryDTO newCategory = categoryService.AddCategory(CategoryName);
				CategoryViewModels.Add(new CategoryViewModel(newCategory, DeleteCategoryCommand));
			});
			this.DeleteCategoryCommand = new RelayCommand<CategoryViewModel>((category) =>
			{
				categoryService.DeleteCategory(category.Category);
				CategoryViewModels.Remove(category);
			});

			this.CategoryViewModels = new ObservableCollection<CategoryViewModel>();
		}

		public ICommand GoBackCommand
		{
			get;
		}

		public ICommand AddCategoryCommand
		{
			get;
		}

		public ICommand DeleteCategoryCommand
		{
			get;
		}

		public string CategoryName
		{
			get { return categoryName; }
			set
			{
				categoryName = value;
				RaisePropertyChanged(nameof(CategoryName));
			}
		}

		public ObservableCollection<CategoryViewModel> CategoryViewModels
		{
			get;
		}

		public void Refresh()
		{
			foreach (CategoryDTO category in categoryService.GetCategories().Where(c => c.Name != "Other"))
			{
				CategoryViewModels.Add(new CategoryViewModel(category, DeleteCategoryCommand));
			}
		}
	}
}
