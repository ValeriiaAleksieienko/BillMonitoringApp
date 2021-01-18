using BillMonitoring.Client.Contracts;
using BillMonitoring.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace BillMonitoring.Client.Services
{
	public class PeriodService : IPeriodService
	{
		private readonly IAuthenticationService authenticationService;

		public PeriodService(IAuthenticationService authenticationService)
		{
			this.authenticationService = authenticationService;
		}

		public PeriodDTO AddPeriod(DateTime startDate, DateTime endDate, double expectedAmount)
		{
			PeriodDTO periodDTO = new PeriodDTO
			{
				UserId = authenticationService.UserId,
				StartDate = startDate,
				EndDate = endDate,
				ExpectedAmount = expectedAmount
			};

			var response = authenticationService.Client.PostAsJsonAsync("api/period/addNewPeriod", periodDTO);
			PeriodDTO newPeriod;
			if (response.Result.IsSuccessStatusCode)
			{
				newPeriod = response.Result.Content.ReadAsAsync<PeriodDTO>().Result;
			}
			else
			{
				newPeriod = null;
			}

			return newPeriod;
		}

		public async void AutoClosePeriod()
		{
			var requestMessage = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri(authenticationService.Client.BaseAddress + "api/period/autoClosePeriod/"),
				Content = new StringContent(
					JsonConvert.SerializeObject(authenticationService.UserId),
					Encoding.UTF8, "application/json")
			};
			await authenticationService.Client.SendAsync(requestMessage);
		}

		public async void ClosePeriod(PeriodDTO period)
		{
			 await authenticationService.Client.PostAsJsonAsync("api/period/ClosePeriod", period);
		}

		public double CountTotalAmount(PeriodDTO period)
		{
			double sum = 0;
			
			foreach(BillDTO bill in period.Bills.ToList())
			{
				sum += bill.Amount;
			}

			return sum;
		}

		public double CountTotalAmountByCategory(PeriodDTO period, CategoryDTO category)
		{
			double sum = 0;

			foreach (BillDTO bill in period.Bills.Where(b => b.CategoryId == category.Id).ToList())
			{
				sum += bill.Amount;
			}

			return sum;
		}

		public async void DeletePeriod(PeriodDTO period)
		{
			await authenticationService.Client.PostAsJsonAsync("api/period/deletePeriod", period);
		}

		public async void EditPeriod(PeriodDTO period)
		{
			await authenticationService.Client.PutAsJsonAsync("api/period/editPeriod", period);
		}

		public IEnumerable<PeriodDTO> GetClosedPeriods()
		{
			var requestMessage = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri(authenticationService.Client.BaseAddress + "api/period/getClosedPeriods/"),
				Content = new StringContent(
					JsonConvert.SerializeObject(authenticationService.UserId),
					Encoding.UTF8, "application/json")
			};
			var response = authenticationService.Client.SendAsync(requestMessage).Result;
			
			return response.Content.ReadAsAsync<IEnumerable<PeriodDTO>>().Result;
		}

		public PeriodDTO GetCurrentPeriod()
		{
			var requestMessage = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri(authenticationService.Client.BaseAddress + "api/period/getCurrentPeriod/"),
				Content = new StringContent(
					JsonConvert.SerializeObject(authenticationService.UserId), 
					Encoding.UTF8, "application/json")
			};

			var response = authenticationService.Client.SendAsync(requestMessage).Result;
			return response.Content.ReadAsAsync<PeriodDTO>().Result;
		}
	}
}
