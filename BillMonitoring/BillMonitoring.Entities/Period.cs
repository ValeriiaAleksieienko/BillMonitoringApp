using BillMonitoring.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillMonitoring.Entities
{
	public class Period
	{
		public Guid Id { get; set; }

		public string UserId { get; set; }
		public ApplicationUser User { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public double ExpectedAmount { get; set; }

		public bool IsClosed { get; set; }

		public IEnumerable<Bill> Bills { get; set; }
	}
}
