using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BillMonitoring.Client.Contracts
{
	public interface IAuthenticationService
	{
		string UserId { get; }

		HttpClient Client { get; }

		string JWT { get; }

		bool IsAuthorized { get; }

		Task<bool> Login(object model);

		Task<JObject> Register(object model);

		//Task<bool> Logout(object model);

	}
}
