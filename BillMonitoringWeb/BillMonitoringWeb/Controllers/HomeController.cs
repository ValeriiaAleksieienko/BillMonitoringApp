using BillMonitoringWeb.Models;
using BillMonitoring.Client.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BillMonitoringWeb.Controllers
{
	public class HomeController : Controller
	{
		IAuthenticationService authenticationService;

		public HomeController(IAuthenticationService authenticationService)
		{
			this.authenticationService = authenticationService;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
