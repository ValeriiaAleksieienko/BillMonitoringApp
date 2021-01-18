using BillMonitoring.Client.Contract;
using BillMonitoringWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BillMonitoringWeb.Controllers
{
	public class AccountController : Controller
	{
		private readonly IAuthenticationService authenticationService;

		public AccountController(IAuthenticationService authenticationService)
		{
			this.authenticationService = authenticationService;
		}

		public IActionResult LoginView()
		{
			return View();
		}

		public async Task<IActionResult> Login(LoginModel loginModel)
		{
			if (await authenticationService.Login(loginModel))
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				TempData["message"] = $"<script>alert('Wrong User Name or Password!')</script>";
				return View("LoginView", loginModel);
			}
		}

		public IActionResult RegistrationView()
		{
			return View();
		}

		public async Task<IActionResult> Register(RegisterModel registerModel)
		{
			Dictionary<string, string> response = await authenticationService.Register(registerModel);
			
			registerModel.ResponseMessage = response["message"];
			TempData["msg"] = $"<script>alert('{response["message"]}')</script>";

			if (response["status"] == "Success")
			{
				return View("RegistrationView");
			}
			else
			{
				return View("RegistrationView", registerModel);
			}
		}
	}
}
