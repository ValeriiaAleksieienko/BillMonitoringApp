using System;
using System.Collections.Generic;
using System.Text;

namespace BillMonitoring.DTO
{
	public class BillDTO
	{
		public Guid Id { get; set; }

		public string UserId { get; set; }

		public Guid PeriodId { get; set; }

		public DateTime Date { get; set; }

		public double Amount { get; set; }

		public string Info { get; set; }

		public Guid CategoryId { get; set; }
		//public Category Category { get; set; }
	}
}
