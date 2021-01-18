using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillMonitoring.Entities.Authentication
{
	public class ApplicationUser: IdentityUser
	{
		public override string Id { get; set; }

		public IEnumerable<Period> Periods { get; set; }

		public IEnumerable<Bill> Bills { get; set; }

		public IEnumerable<Category> Categories { get; set; }
	}
}
