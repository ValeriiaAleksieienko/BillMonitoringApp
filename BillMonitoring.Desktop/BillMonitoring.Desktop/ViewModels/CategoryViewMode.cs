using BillMonitoring.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BillMonitoring.Desktop.ViewModels
{
	internal class CategoryViewModel : BaseViewModel
	{
		private CategoryDTO category;

		public CategoryViewModel(CategoryDTO category, ICommand command)
		{
			this.category = category;
			this.DeleteCategoryCommand = command;
		}

		public CategoryDTO Category
		{
			get { return category; }
			set
			{
				category = value;
				RaisePropertyChanged(nameof(Category));
			}
		}

		public ICommand DeleteCategoryCommand
		{
			get;
		}
	}
}
