using BillMonitoring.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillMonitoring.Entities
{
	public class Bill
	{
		public Guid Id { get; set; }

		public string UserId { get; set; }
		public ApplicationUser User { get; set; }

		public Guid PeriodId { get; set; }
		public Period Period { get; set; }

		public DateTime Date { get; set; }

		public double Amount { get; set; }

		public string Info { get; set; }

		public Category Category { get; set; }
	}
}
