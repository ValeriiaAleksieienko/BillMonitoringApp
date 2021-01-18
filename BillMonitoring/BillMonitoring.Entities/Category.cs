using BillMonitoring.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillMonitoring.Entities
{
	public class Category
	{
		public Guid Id { get; set; }

		public string UserId { get; set; }
		public ApplicationUser User { get; set; }

		public string Name { get; set; }

		public IEnumerable<Bill> Bills { get; set; }

		//public string ReportColor { get; set; }
	}
}
