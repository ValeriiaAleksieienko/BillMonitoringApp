using BillMonitoring.Client.Contracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BillMonitoring.Client
{
	public class AuthenticationService : IAuthenticationService
	{
		public string UserId { get; private set; }

		public HttpClient Client { get; }

		public string JWT { get; private set; }

		public bool IsAuthorized { get; private set; }

		public AuthenticationService()
		{
			Client = new HttpClient();
			Client.BaseAddress = new Uri("https://localhost:44321/");
			
			Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			IsAuthorized = false;
		}

		public async Task<bool> Login(object model)
		{
			var response = await Client.PostAsJsonAsync("api/authenticate/login", model);
			if (response.IsSuccessStatusCode)
			{
				var resp = JObject.Parse(response.Content.ReadAsStringAsync().Result);

				UserId = resp["id"].ToString();
				JWT = resp["jwt"].ToString();
				IsAuthorized = true;

				Client.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue("Bearer", JWT);
			}

			return response.IsSuccessStatusCode;
		}

		public async Task<JObject> Register(object model)
		{
			var response = await Client.PostAsJsonAsync("api/authenticate/register", model);
			//Dictionary<string, string> resp = response.Content.ReadAsAsync<Dictionary<string, string>>().Result;
			var resp = JObject.Parse(response.Content.ReadAsStringAsync().Result);
			return resp;
		}
	}
}
