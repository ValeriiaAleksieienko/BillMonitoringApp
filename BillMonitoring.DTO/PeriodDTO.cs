using System;
using System.Collections.Generic;
using System.Text;

namespace BillMonitoring.DTO
{
	public class PeriodDTO
	{
		public Guid Id { get; set; }

		public string UserId { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public double ExpectedAmount { get; set; }

		public bool IsClosed { get; set; }

		public IEnumerable<BillDTO> Bills { get; set; }
	}
}
