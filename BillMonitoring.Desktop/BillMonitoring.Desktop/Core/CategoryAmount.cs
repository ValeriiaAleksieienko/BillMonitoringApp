using BillMonitoring.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillMonitoring.Desktop.Core
{
	public class CategoryAmount
	{
		public CategoryDTO Category { get; set; }

		public double Amount { get; set; }
	}
}
