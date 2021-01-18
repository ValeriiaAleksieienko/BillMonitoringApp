using BillMonitoring.Client.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillMonitoring.Client.Services
{
	public class BillService : IBillService
	{
		private readonly IAuthenticationService authenticationService;

		public BillService(IAuthenticationService authenticationService)
		{
			this.authenticationService = authenticationService;
		}
	}
}
